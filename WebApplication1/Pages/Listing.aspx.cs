using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Models.Repository;

namespace SportsStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        //facciamo riferiemnto alla classe Repository che gestisce tutto il DbContext
        private Repository repo = new Repository();
        private int pageSize = 4;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        // Metodo che restituisce una collezione di oggetti Product
        protected IEnumerable<Product> GetProducts()
        {
            // Restituisce la proprietà Products del repository
            // Presumibilmente, il repository contiene la logica per recuperare la lista dei prodotti
            return repo.Products

                // I prodotti vengono ordinati per ProductID, quindi viene eseguita una paginazione
                .OrderBy(p => p.ProductID)

                // selezionando solo i prodotti nella pagina corrente utilizzando Skip e Take
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }

        protected int CurrentPage 
        { get 
            { 
                int page;

                // Ottiene il numero di pagina corrente dalla stringa di query "page" dell'URL
                // Se la conversione riesce, restituisce il numero di pagina
                // Altrimenti, restituisce 1 come valore predefinito

                // Va a generare gli altri prodotti, perchè la pagina è stata tagliata
                page = int.TryParse(Request.QueryString["page"], out page) ? page:1;

                // Questo permette che se viene ricarcato una pagine inesistente, ritorna all'ultima pagina
                return page > MaxPage ? MaxPage : page;
            } 
        }

        protected int MaxPage
        {
            get     
            {
                return (int)Math.Ceiling((decimal)repo.Products.Count() / pageSize);
            }

        }
    }
}