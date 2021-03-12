using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TaskWebJob.Data;
using TaskWebJob.Models;

namespace TaskWebJob.Repositories
{
    public class RepositoryWebJob
    {
        WebJobContext context;
        public RepositoryWebJob(WebJobContext context)
        {
            this.context = context;
        }

        public List<NoticiaRss> GetRss()
        {
            String url = "https://www.chollometro.com/rss";
            XDocument docxml = XDocument.Load(url);
            var consulta = from datos in docxml.Descendants("item")
                           select new NoticiaRss
                           {
                               Title = datos.Element("title").Value,
                               Link = datos.Element("link").Value,
                               Description = datos.Element("description").Value
                           };
            return consulta.ToList();
        }

        private int GetMaxId()
        {
            if (this.context.WebJobs.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.WebJobs.Max(x => x.IdTitular) + 1;
            }
        }

        public void PopulateDataWebJob()
        {
            List<NoticiaRss> noticias = this.GetRss();
            int id = this.GetMaxId();
            foreach (NoticiaRss rss in noticias)
            {
                WebJob webJob = new WebJob();
                webJob.IdTitular = id;
                webJob.Titulo = rss.Title;
                webJob.Enlace = rss.Link;
                webJob.Descripcion = rss.Description;
                webJob.Fecha = DateTime.Now;
                id += 1;
                this.context.WebJobs.Add(webJob);
            }
            this.context.SaveChanges();
        }
    }
}
