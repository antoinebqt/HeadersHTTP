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
    internal class Question3
    {

        public static string ComputeScenario1Stats(string root)
        {
            // Liste pour stocker les âges des pages
            List<double> ages = new List<double>();

            // Liste pour stocker les longueurs de contenu des pages
            List<double> contentLengths = new List<double>();

            List<string> serverUrls = null;

            // Construction du chemin absolu du fichier csv avec les urls des sites
            string filePath = Path.Combine(root, "websiteUrls/wikipediaUrls.txt");

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

                    // Récupérer la longueur du contenu de la page à partir de l'en-tête de réponse
                    string pageContentLength = response.Headers["Content-Length"];

                    if (pageAge != null)
                    {
                        Console.Write(" - \"Age\" header found");

                        // Convertir l'âge de la page en double et l'ajouter à la liste
                        double ageInSeconds = double.Parse(pageAge);
                        ages.Add(ageInSeconds);
                    }
                    else
                    {
                        Console.Write(" - No \"Age\" header found");
                    }

                    if (pageContentLength != null)
                    {
                        Console.Write(" - \"Content-Length\" header found");

                        // Convertir la longueur du contenu de la page en double et l'ajouter à la liste
                        double contentLength = double.Parse(pageContentLength);
                        contentLengths.Add(contentLength);
                    }
                    else
                    {
                        Console.Write(" - No \"Content-Length\" header found");
                    }

                    // Fermer la réponse HTTP
                    response.Close();

                }
                catch (WebException ex)
                {
                    Console.Write(" - " + ex.ToString());
                    continue;
                }

                Console.Write("\n");
            }

            // Calculer la moyenne de l'age
            double ageAverage = ages.Average();

            // Calculer l'écart type de l'age
            double ageVariance = ages.Select(age => Math.Pow(age - ageAverage, 2)).Average();
            double ageStdDev = Math.Sqrt(ageVariance);

            // Calculer le total des longueurs de contenu des pages
            double contentLengthTotal = contentLengths.Sum();

            // Calculer la moyenne des longueurs de contenu des pages
            double contentLengthAverage = contentLengths.Average();

            // Calculer l'écart type de l'age
            double contentLengthVariance = contentLengths.Select(cl => Math.Pow(cl - contentLengthAverage, 2)).Average();
            double contentLengthStdDev = Math.Sqrt(contentLengthVariance);

            // Créer un objet anonyme pour stocker les statistiques
            var statsObject = new
            {
                avg_age = ageAverage,
                std_dev_age = ageStdDev,
                total_length = contentLengthTotal,
                avg_length = contentLengthAverage,
                std_dev_length = contentLengthStdDev
            };

            // Sérialiser l'objet anonyme en JSON
            string statsJson = JsonConvert.SerializeObject(statsObject);

            // Retourner la chaîne de caractères JSON avec les statistiques
            return statsJson;
        }

        public static string ComputeScenario2Stats(List<string> serverUrls)
        {
            // Liste pour stocker les longueurs des cookies des pages
            List<double> cookieLengths = new List<double>();

            // Parcourir la liste des adresses URL des pages de Polytech Nice Sophia
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

                    // Récupérer les cookies de la page à partir de l'en-tête de réponse
                    string pageCookie = response.Headers["Set-Cookie"];

                    if (pageCookie != null)
                    {
                        Console.Write(" - \"Set-Cookie\" header found");

                        string[] cookieFields = pageCookie.Split(';');
                        cookieLengths.Add(cookieFields.Length);
                    }
                    else
                    {
                        Console.Write(" - No \"Set-Cookie\" header found");
                    }

                    // Fermer la réponse HTTP
                    response.Close();

                }
                catch (WebException ex)
                {
                    Console.Write(" - " + ex.ToString());
                    continue;
                }

                Console.Write("\n");
            }

            // Calculer le total des longueurs des cookies des pages
            double cookieLengthTotal = cookieLengths.Sum();

            // Calculer la moyenne des longueurs de contenu des pages
            double cookieLengthAverage = cookieLengths.Average();

            // Calculer l'écart type de l'age
            double cookieLengthVariance = cookieLengths.Select(cookie => Math.Pow(cookie - cookieLengthAverage, 2)).Average();
            double cookieLengthStdDev = Math.Sqrt(cookieLengthVariance);

            // Créer un objet anonyme pour stocker les statistiques
            var statsObject = new
            {
                total_cookie = cookieLengthTotal,
                avg_cookie = cookieLengthAverage,
                std_dev_cookie = cookieLengthStdDev
            };

            // Sérialiser l'objet anonyme en JSON
            string statsJson = JsonConvert.SerializeObject(statsObject);

            // Retourner la chaîne de caractères JSON avec les statistiques
            return statsJson;
        }

    }
}
