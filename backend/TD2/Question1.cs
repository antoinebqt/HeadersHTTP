using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TD2
{
    internal class Question1
    {
        public static string ComputeServerStats(List<string> serverUrls)
        {
            // Dictionnaire pour stocker les statistiques sur les serveurs Web
            Dictionary<string, int> serverStats = new Dictionary<string, int>();

            // Parcourir la liste des adresses URL des serveurs Web
            for (int i = 0; i < serverUrls.Count; i++)
            {
                string url = serverUrls[i];
                Console.Write("Processing " + url);

                // Créer un objet HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;

                try
                {
                    // Envoyer une requête et récupérer la réponse
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    // Récupérer le type de serveur à partir de l'en-tête de réponse
                    string serverType = response.Headers["Server"];

                    if (serverType != null)
                    {
                        Console.WriteLine(" - \"Server\" header found");

                        // Vérifier si le type de serveur est déjà dans le dictionnaire
                        if (serverStats.ContainsKey(serverType))
                        {
                            // Incrémenter le nombre d'occurrences du type de serveur
                            serverStats[serverType]++;
                        }
                        else
                        {
                            // Ajouter le type de serveur au dictionnaire
                            serverStats.Add(serverType, 1);
                        }
                    }
                    else
                    {
                        Console.WriteLine(" - No \"Server\" header found");
                    }

                    // Fermer la réponse HTTP
                    response.Close();

                }
                catch (WebException ex)
                {
                    Console.WriteLine(" - " + ex.ToString());
                    continue;
                }
            }


            // Créer un objet anonyme pour stocker les statistiques
            var statsObject = new
            {
                stats = serverStats
            };

            // Sérialiser l'objet anonyme en JSON
            string statsJson = JsonConvert.SerializeObject(statsObject);

            // Retourner la chaîne de caractères JSON avec les statistiques
            return statsJson;
        }
    }
}
