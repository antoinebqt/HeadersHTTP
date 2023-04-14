using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TD2
{
    internal class Question2
    {
        public static string ComputeAgeStats(string root)
        {
            // Liste pour stocker les âges des pages
            List<double> ages = new List<double>();

            List<string> serverUrls = null;

            // Construction du chemin absolu du fichier csv avec les urls des sites
            string filePath = Path.Combine(root, "websiteUrls/niceMatinUrls.txt");

            // Création de la liste
            if (System.IO.File.Exists(filePath))
            {
                // Lecture des URLs à partir du fichier texte
                serverUrls = System.IO.File.ReadAllLines(filePath).ToList();
            }

            // Parcourir la liste des adresses URL des pages de Polytech Nice Sophia
            for (int i = 0; i < serverUrls.Count; i++)
            {
                string url = serverUrls[i];
                Console.Write("Processing " + url);

                // Créer un objet HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 10000;

                try
                {
                    // Envoyer une requête et récupérer la réponse
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    // Récupérer l'age de la page à partir de l'en-tête de réponse
                    string pageAge = response.Headers["Age"];

                    if (pageAge != null)
                    {
                        Console.WriteLine(" - \"Age\" header found");

                        // Convertir l'âge de la page en double et l'ajouter à la liste
                        double ageInSeconds = double.Parse(pageAge);
                        ages.Add(ageInSeconds);
                    }
                    else
                    {
                        Console.WriteLine(" - No \"Age\" header found");
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

            // Calculer la moyenne
            double ageAverage = ages.Average();

            // Calculer l'écart type
            double ageVariance = ages.Select(age => Math.Pow(age - ageAverage, 2)).Average();
            double ageStdDev = Math.Sqrt(ageVariance);

            // Créer un objet anonyme pour stocker les statistiques
            var statsObject = new
            {
                average = ageAverage,
                std_dev = ageStdDev
            };

            // Sérialiser l'objet anonyme en JSON
            string statsJson = JsonConvert.SerializeObject(statsObject);

            // Retourner la chaîne de caractères JSON avec les statistiques
            return statsJson;
        }
    }
}
