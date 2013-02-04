using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using EntitiesLogic.Entities;

namespace Trellol.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString DisplayNameFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData);
            return MvcHtmlString.Create(metaData.GetDisplayName());
        }

        public static MvcHtmlString DisplayNameFor<TModel, TValue>(
            this HtmlHelper<IEnumerable<TModel>> html,
            Expression<Func<TModel, TValue>> expression)
        {
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            return MvcHtmlString.Create(metaData.GetDisplayName());
        }

        public static MvcHtmlString DeleteListRouteLink<T>(this HtmlHelper html, IEnumerable<T> cards, string linkText, string routeName, object routeValues)
        {
            if (cards == null) throw new ArgumentNullException();
                
            if(cards.Count() == 0)
                return html.RouteLink(linkText, routeName, routeValues);
            else
                return MvcHtmlString.Empty;    
        }

        

        public static MvcHtmlString DisplayImage(this HtmlHelper html, User user, string imageUrl)
        {
            
            if (!user.isConfirmed || user.ImageProfile == null)
                return MvcHtmlString.Empty;

            //    <img width="150" height="150"
            //        src="@Url.Action("GetImage", "Profile", new { id = Model.Username })" />
            UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            string url = urlHelper.Action("GetImage", "Profile", new { id = user.Username });

            StringBuilder result = new StringBuilder();
            TagBuilder tag = new TagBuilder("img");
            tag.MergeAttribute("src", url);
            tag.AddCssClass("upload_image_input");
            result.Append(tag.ToString());


            return MvcHtmlString.Create(result.ToString());            
        }

        public static MvcHtmlString InputImage(this HtmlHelper html, bool userIsConfirmed)
        {

            if (!userIsConfirmed) return MvcHtmlString.Empty;

            //Upload new image: <input type="file" name="Image" accept="image/*" />
            StringBuilder result = new StringBuilder();
            result.Append("Upload new image:");
            TagBuilder tag = new TagBuilder("input");
            tag.MergeAttribute("type", "file");
            tag.MergeAttribute("name", "Image");
            tag.MergeAttribute("accept", "image/*");
            result.Append(tag.ToString());

            return MvcHtmlString.Create(result.ToString());
            
        }
    }
}