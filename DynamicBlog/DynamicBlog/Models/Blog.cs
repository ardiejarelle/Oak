﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oak;
using System.Web.Mvc;

namespace DynamicBlog.Models
{
    public class Blog : Mix
    {
        Comments comments;

        public Blog()
            : base(new { title = "", body = "" })
        {
            comments = new Comments();
        }

        public Blog(object valueType)
            : base(valueType)
        {
            comments = new Comments();
        }

        public bool IsValid()
        {
            if (!CheckTitle())
                return false;

            return true;

        }

        private bool CheckTitle()
        {
            if (string.IsNullOrEmpty(MixWith.Title))
            {
                return false;
            }

            return true;
        }

        public string Message()
        {
            if(!IsValid())
            {
                return "Invalid Blog, please correct errors and try again.";
            }

            return "";
        }

        public void AddComment(string comment)
        {
            comments.Insert(new { BlogId = MixWith.Id, Text = comment });
        }

        public List<dynamic> Comments()
        {
            return (comments.All("BlogId = @0", "", 0, "*", MixWith.Id) as IEnumerable<dynamic>).ToList();
        }

        public string Summary
        {
            get
            {
                if (MixWith.Body == null) return "";

                if (MixWith.Body.Length > 50) return MixWith.Body.Substring(0, 50);

                return MixWith.Body;
            }
        }

        public MvcHtmlString Title_TextBox
        {
            get
            {
                var tb = new TagBuilder("input");
                tb.Attributes.Add("id", "Title");
                tb.Attributes.Add("name", "Title");

                return MvcHtmlString.Create(tb.ToString());
            }
        }

        public MvcHtmlString Title_ValidationMessage
        {
            get
            {
                if (CheckTitle())
                {
                    return null;
                }
                else
                {
                    var tb = new TagBuilder("label");


                    return MvcHtmlString.Create(tb.ToString());
                }
            }
        }

        public MvcHtmlString Body_TextBox
        {
            get
            {
                var tb = new TagBuilder("textarea");
                tb.Attributes.Add("id", "Body");
                tb.Attributes.Add("name", "Body");

                return MvcHtmlString.Create(tb.ToString());
            }
        }
    }
}