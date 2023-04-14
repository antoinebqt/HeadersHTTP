using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using static System.Net.WebRequestMethods;

namespace TD2
{
    internal class Program
    {
        // Liste des adresses URL des serveurs Web
        private static List<string> serverUrls = new List<string>()
        {
            "https://polytech.univ-cotedazur.fr/",
            "https://twitter.com/home"
        };

        private static string httpRoot;

        static void Main(string[] args)
        {

            // Déclaration de la variable d'environnement HTTP_ROOT
            httpRoot = Environment.GetEnvironmentVariable("HTTP_ROOT");

            // Si la variable n'est pas définie, on utilise le dossier courant comme racine
            if (string.IsNullOrEmpty(httpRoot))
            {
                httpRoot = Directory.GetCurrentDirectory();
            }
            
            // Construction du chemin absolu du fichier csv avec les urls des sites
            string filePath = Path.Combine(httpRoot, "websiteUrls/topDomainUrls.txt");

            // Création de la liste
            if (System.IO.File.Exists(filePath))
            {
                // Lecture des URLs à partir du fichier texte et ajout a la liste existante
                serverUrls.AddRange(System.IO.File.ReadAllLines(filePath).ToList());
            }

            // Création de l'objet HttpListener (serveur)
            HttpListener listener = new HttpListener();

            // Ajout de l'adresse du serveur
            listener.Prefixes.Add("http://localhost:8080/");

            // Démarrage du serveur
            listener.Start();
            Console.WriteLine("Serveur démarré sur http://localhost:8080/");

            while (true)
            {
                // Attendre une nouvelle requête
                HttpListenerContext context = listener.GetContext();

                // Récupération du chemin relatif de la requête
                string relativePath = context.Request.RawUrl.TrimStart('/');

                // Création de la réponse
                string responseString = null;

                // Definition des differents endpoint du serveur
                if (relativePath == "server")
                {
                    responseString = Question1.ComputeServerStats(serverUrls);
                } else if (relativePath == "age")
                {
                    responseString = Question2.ComputeAgeStats(httpRoot);
                } else if (relativePath == "scenario1")
                {
                    responseString = Question3.ComputeScenario1Stats(httpRoot);
                } else if (relativePath == "scenario2")
                {
                    responseString = Question3.ComputeScenario2Stats(serverUrls);
                }

                Console.WriteLine("\nRéponse envoyée : \n" + responseString + "\n");

                // Conversion de la réponse en octets
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseString);

                // Ajout de l'en-tête CORS
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                // Envoi de la réponse
                context.Response.ContentLength64 = responseBytes.Length;
                context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            }
        }
    }    
}
