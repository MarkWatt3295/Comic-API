using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comic_API
{
    public  class ComicProcessor
    {
      

        //Using async as we dont know how long it will take the webpages to return the data
        public static async Task<ComicModel> LoadComic(int comicNumber = 0)
        {
            string url = "";

            if(comicNumber > 0)
            {
                url = $"http://xkcd.com/{comicNumber}/info.0.json ";
            }

            else
            {
                url = "http://xkcd.com/info.0.json";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                //If successful read the data back
                if (response.IsSuccessStatusCode)
                {
                    //Json converter isn't case sensitive but names need to match up with what is being searched.
                    //ReadAsAsyn - uses newton json converter, it takes in the json data and trys to map it to model given
                    ComicModel comic = await response.Content.ReadAsAsync<ComicModel>();


                    return comic;
                }

                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
