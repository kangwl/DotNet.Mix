using System.Web;

namespace XK.DataApi.Source.Act {
    public interface IAct {
        string Add(HttpContext context);
        string List(HttpContext context);
        string Delete(HttpContext context);
        string Update(HttpContext context);
        string GetOne(HttpContext context);
    }
}