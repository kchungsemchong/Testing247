using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Testing247Media
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var existingArchiveDictionary = new Dictionary<string, string>();
            //existingArchiveDictionary = (Dictionary<string, string>)Session["ArchiveSession"];

            //if (existingArchiveDictionary == null || existingArchiveDictionary.Count <= 0)
            //{
            //    Dictionary<string, string> newArchiveDictionary = new Dictionary<string, string>();
            //    Session["ArchiveSession"] = newArchiveDictionary;
            //}
        }

        public void StoreSiteSession()
        {
            string key = "SiteId";
            if (!String.IsNullOrEmpty(txtSite.Text))
            {
                StoreInSession(key, txtSite.Text);
            }
        }

        public void StoreArticleSession()
        {
            string key = "ArticleType";
            if (!String.IsNullOrEmpty(txtArticle.Text))
            {
                StoreInSession(key, txtArticle.Text);
            }
        }

        protected void txtBox_TextChanged(object sender, EventArgs e)
        {
            StoreSiteSession();
            StoreArticleSession();
        }

        private void StoreInSession(string key, string value)
        {
            var archiveDictionary = (Dictionary<string, string>)Session["ArchiveSession"];

            if (archiveDictionary == null)
            {
                Dictionary<string, string> newArchiveDictionary = new Dictionary<string, string>();
                newArchiveDictionary.Add(key, value);
                Session["ArchiveSession"] = newArchiveDictionary;
            }

            if (archiveDictionary != null)
            {
                if (!(archiveDictionary.Count < 1))
                {
                    if (archiveDictionary.ContainsKey(key))
                        archiveDictionary[key] = value;

                    if (!(archiveDictionary.ContainsKey(key)))
                        archiveDictionary.Add(key, value);
                }

                if (archiveDictionary.Count == 0)
                {
                    archiveDictionary.Add(key, value);
                }

                Session["ArchiveDictionary"] = archiveDictionary;
            }
        }
    }
}