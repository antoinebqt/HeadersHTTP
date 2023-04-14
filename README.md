
# TD : Exploitation des champs d'en-tête HTTP.

## Comment lancer le projet

- Ouvrir le fichier `index.html` qui se trouve dans le dossier frontend.

- Ouvrir la solution `TD2.sln` (dans Microsoft Visual Studio Code par exemple) qui se trouve dans le dossier backend.

- Exécuter la solution `TD2.sln` pour lancer le serveur.
A ce moment-là un terminal devrait s'ouvrir et indiquer `"Serveur démarré sur http://localhost:8080/"` :

![](https://cdn.discordapp.com/attachments/1091340351991709807/1096447518067392593/2023-04-14_16h50_51.jpg)

## Comment utiliser le projet

Une fois le serveur en cours d'exécution, il suffit de cliquer sur les boutons `"Calculer les stats"` sur la page `index.html`.
Vous pouvez alors regarder dans le terminal du serveur les différentes requêtes qui s'envoient.

## Comment ajouter ses propres URLs ?

Tout en haut du fichier `Program.cs` de la solution `TD2.sln`, il est possible d'ajouter ses propres URLs dans la liste `serverUrls` comme ceci :

![](https://cdn.discordapp.com/attachments/1091340351991709807/1096446739843665971/2023-04-14_16h47_44.jpg)

Les adresses URLs s'ajouteront aux adresses utilisées de base qui sont répertoriées dans ce [fichier](https://github.com/antoinebqt/HeadersHTTP/blob/master/backend/TD2/bin/Debug/websiteUrls/topDomainUrls.txt).

## Justifications Question 3

Pour la question 3 j'ai choisi de créer 2 scénarios différents pour faire des statistiques sur de nouveaux headers.

### Scénario 1 : Longueur du contenu

Je trouve les statistiques sur la longueur du contenu pertinente car elle permet de mesurer la taille moyenne des pages web demandées. Cela peut aider à détecter des pages anormalement longues ou courtes qui peuvent indiquer des problèmes de performance ou de qualité de contenu. Cela peut également aider à optimiser les processus de mise en cache ou à évaluer l'efficacité des algorithmes de compression.

### Scénario 2 : Nombre de cookies

La statistique sur le nombre de cookies est également pertinente car elle peut aider à détecter des pratiques de suivi et de collecte de données excessives ou indésirables. En effet, les cookies sont souvent utilisés pour stocker des informations de session ou de préférences utilisateur, mais ils peuvent également être utilisés pour suivre la navigation de l'utilisateur ou pour collecter des données personnelles. En surveillant le nombre de cookies présents dans les entêtes HTTP, on peut détecter des comportements suspects et éventuellement prendre des mesures pour protéger la vie privée des utilisateurs. Cela devient suspect lorsqu'une page demande à enregistrer dans les cookies un trop grand nombre de choses.
