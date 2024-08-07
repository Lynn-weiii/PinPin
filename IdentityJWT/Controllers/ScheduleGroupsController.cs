﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinPinServer.Models;
using PinPinServer.Models.DTO;
using PinPinServer.Services;

namespace PinPinTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleGroupsController : ControllerBase
    {
        private PinPinContext _context;
        private readonly AuthGetuserId _getUserId;

        public ScheduleGroupsController(PinPinContext context, AuthGetuserId getuserId)
        {
            _context = context;
            _getUserId = getuserId;
        }

        //GET:api/ScheduleGroups
        [HttpGet]
        public async Task<ActionResult<Dictionary<int, string>>> GetScheduleGroups(int schedule_id)
        {
            int? userID = _getUserId.PinGetUserId(User).Value;
            if (userID == null || userID == 0) return BadRequest("Invalid user ID");

            try
            {
                Dictionary<int, string> groupUsers = await _context.ScheduleGroups
                .Where(group => group.ScheduleId == schedule_id && group.LeftDate == null)
                .Include(group => group.User)
                .ToDictionaryAsync(
                    group => group.UserId,
                    group => group.User.Name
                );

                if (!groupUsers.ContainsKey(userID.Value)) return Forbid("You can't search not your group");

                return Ok(groupUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        //GET:api/ScheduleGroups/GetGropsMembers/${gschedule_id}
        [HttpGet("GetGroupsMembers/{gschedule_id}")]
        public async Task<ActionResult<List<GroupDTO>>> GetGroupsMembers(int gschedule_id)
        {

            int? jwtuserID = _getUserId.PinGetUserId(User).Value;
            if (jwtuserID == null || jwtuserID == 0)
            {
                return BadRequest();
            }

            try
            {
                var groupDTOs = await _context.ScheduleGroups
                    .Where(g => g.ScheduleId == gschedule_id && g.LeftDate == null && g.UserId != jwtuserID) // 排除 JWT 用户
                    .Include(g => g.User)
                    .ThenInclude(u => u.ScheduleAuthorities)
                    .GroupBy(g => g.UserId)
                    .Select(g => new
                    {
                        UserId = g.Key,
                        UserName = g.First().User.Name,
                        UserPhoto = g.First().User.Photo,
                        AuthorityIds = g.First().User.ScheduleAuthorities
                            .Where(a => a.ScheduleId == gschedule_id)
                            .Select(a => a.AuthorityCategoryId)
                            .ToList()
                    }).ToListAsync();

                bool userCanRemove = await _context.ScheduleAuthorities
                    .AnyAsync(sa => sa.ScheduleId == gschedule_id && sa.AuthorityCategoryId == 3 && sa.UserId == jwtuserID);

                int hostID = await _context.Schedules
                    .Where(s => s.Id == gschedule_id)
                    .Select(s => s.UserId)
                    .FirstOrDefaultAsync();

                var finalGroupDTOs = groupDTOs
                    .Select(member => new GroupDTO
                    {
                        UserId = member.UserId,
                        UserName = member.UserName,
                        UserPhoto = member.UserPhoto,
                        AuthorityIds = member.AuthorityIds,
                        CanRemove = userCanRemove && member.UserId != hostID // 只有具有移除权限且不是主办人的成员才能移除
                    })
                    .ToList();

                if (finalGroupDTOs.Any())
                {
                    return Ok(finalGroupDTOs);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        //POST:api/ScheduleGroups/Invitemember
        [HttpPost("Invitemember")]
        public async Task<IActionResult> Invitemember([FromBody] InviteMemeberDTO inviteMemeberDTO)
        {
            try
            {
                var member = await _context.Users
                .Where(u => u.Email == inviteMemeberDTO.Email)
                .Select(u => new { u.Id })
                .FirstOrDefaultAsync();

                if (member == null)
                {
                    return BadRequest(new { message = "無此會員" });
                }

                bool isHoster = await _context.ScheduleGroups
                    .AnyAsync(s => s.ScheduleId == inviteMemeberDTO.ScheduleId && s.UserId == member.Id && s.IsHoster == true);
                if (isHoster)
                {
                    return BadRequest(new { message = "主辦者不需加入群組" });
                }

                bool isGroupMember = await _context.ScheduleGroups
                    .AnyAsync(sg => sg.UserId == member.Id && sg.ScheduleId == inviteMemeberDTO.ScheduleId && sg.LeftDate == null);
                if (isGroupMember)
                {
                    return BadRequest(new { message = "會員已加入此群組" });
                }

                var wasmember = await _context.ScheduleGroups.FirstOrDefaultAsync(s => s.ScheduleId == inviteMemeberDTO.ScheduleId && s.UserId == member.Id && s.LeftDate.HasValue);

                if (wasmember != null)
                {
                    wasmember.LeftDate = null;
                    _context.ScheduleGroups.Update(wasmember);
                    await _context.SaveChangesAsync();

                    var updateMemberAuthority = new ScheduleAuthority
                    {
                        ScheduleId = inviteMemeberDTO.ScheduleId,
                        UserId = member.Id,
                        AuthorityCategoryId = inviteMemeberDTO.AuthorityCategoryId
                    };
                    _context.ScheduleAuthorities.Add(updateMemberAuthority);
                    await _context.SaveChangesAsync();

                    return Ok(wasmember);
                }

                var newmember = new ScheduleGroup
                {
                    Id = 0,
                    ScheduleId = inviteMemeberDTO.ScheduleId,
                    UserId = member.Id,
                    IsHoster = false,
                    JoinedDate = DateTime.Now,
                    LeftDate = null
                };
                _context.ScheduleGroups.Add(newmember);
                await _context.SaveChangesAsync();

                var newmemberauthority = new ScheduleAuthority
                {
                    Id = 0,
                    ScheduleId = inviteMemeberDTO.ScheduleId,
                    UserId = member.Id,
                    AuthorityCategoryId = inviteMemeberDTO.AuthorityCategoryId
                };
                _context.ScheduleAuthorities.Add(newmemberauthority);
                await _context.SaveChangesAsync();

                return Ok(newmember);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseEntry = entry.GetDatabaseValues();
                if (databaseEntry == null)
                {
                    return Ok();
                }
                else
                {
                    var updatedData = (ScheduleGroup)databaseEntry.ToObject();
                    _context.Entry(updatedData).OriginalValues.SetValues(entry.OriginalValues);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "已新增成員!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "伺服器錯誤", detail = ex.Message });
            }
        }



        //PUT:api/ScheduleGroups/Memberexit
        [HttpPut("Memberexit")]
        public async Task<ActionResult> Memberexit([FromBody] exitmemberDTO exitDTO)
        {
            int userId = exitDTO.UserID;
            var ismember = await _context.ScheduleGroups
                .FirstOrDefaultAsync(s => s.UserId == userId && s.ScheduleId == exitDTO.ScheduleID && s.LeftDate == null);

            if (ismember == null)
            {
                return BadRequest("無此成員");
            }

            try
            {
                ismember.LeftDate = DateTime.Now;
                _context.ScheduleGroups.Update(ismember);
                await _context.SaveChangesAsync();

                var removememberauth = await _context.ScheduleAuthorities
                    .Where(sa => sa.UserId == userId && sa.ScheduleId == exitDTO.ScheduleID)
                    .ToListAsync();

                if (removememberauth.Any())
                {
                    _context.ScheduleAuthorities.RemoveRange(removememberauth);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { Message = "成員已退出" });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseEntry = entry.GetDatabaseValues();
                if (databaseEntry == null)
                {
                    return Ok();
                }
                else
                {
                    var updatedData = (ScheduleGroup)databaseEntry.ToObject();
                    _context.Entry(updatedData).OriginalValues.SetValues(entry.OriginalValues);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "已新增成員!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "伺服器錯誤", detail = ex.Message });
            }
        }
    }
}