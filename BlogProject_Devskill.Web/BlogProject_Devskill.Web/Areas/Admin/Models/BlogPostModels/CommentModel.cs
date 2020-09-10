using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Framework.Services.PostServices;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.BlogPostModels
{
    public class CommentModel : BlogPostBaseModel
    {
        public CommentModel() : base()
        {

        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Description { get; set; }
        public int BlogPostId { get; set; }
        public DateTime CommentTime { get; set; }
        public bool IsAuthorized { get; set; }
        public int BlogId { get; set; }
        public async Task<object> GetCommentsByIdAsync(DataTablesAjaxRequestModel tableModel,int id)
        {
            var result = await _commentService.GetCommentsByIdAsync(
                id,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Title" }),
                tableModel.PageIndex, tableModel.PageSize);
            
            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                            item.Name,
                            item.Email,                   
                            (item.Description.Substring(0,item.Description.Length-1>30?30:item.Description.Length-1)+"...").ToString(),
                            item.CommentTime.ToString(),
                            item.IsAuthorized==true?$"<span class=\"badge badge-pill badge-success\">Approved</span>":$"<span class=\"badge badge-pill badge-danger\">Pending</span>",
                            item.IsAuthorized==false?$"<button type=\"submit\" class=\"btn btn-success btn-sm show-bs-modal2\" href=\"#\" data-id='{item.Id}' value='${item.Id}'><i class=\"fas fa-check\"> </i>Approve</button>"
                                                    :$"<button type=\"submit\" class=\"btn btn-danger btn-sm show-bs-modal3\" href=\"#\" data-id='{item.Id}' value='${item.Id}'><i class=\"fas  fa-times-circle\"> </i>Disapprove</button>",
                            item.Id.ToString()
                        }).ToArray()

            };
        }

        public async Task<string> DeleteAsync(int id)
        {
            var name = await _commentService.DeleteAsync(id);
            return name.Name;
        }

        public async Task LoadByIdAsync(int id)
        {
            var result = await _commentService.GetByIdAsync(id);
            this.Id = result.Id;
            this.Name = result.Name;
            this.Email = result.Email;
            this.Description = result.Description;
            this.BlogPostId = result.BlogPostId;
            this.IsAuthorized = result.IsAuthorized;
            this.CommentTime = result.CommentTime;
        }

        public async Task ApproveAsync(int commentId, bool decision)
        {
            var entity = new Comment
            {
                Id = commentId,
                IsAuthorized = decision
            };
            await _commentService.UpdateAsync(entity);
        }
    }
}
