document.addEventListener('DOMContentLoaded', init, false);

function init() {
    document.getElementById("btnStats-server").addEventListener("click", () => displayStat("server"));
    document.getElementById("btnStats-age").addEventListener("click", () => displayStat("age"));
    document.getElementById("btnStats-scenario1").addEventListener("click", () => displayStat("scenario1"));
    document.getElementById("btnStats-scenario2").addEventListener("click", () => displayStat("scenario2"));

}

async function displayStat(stat) {
    const container = document.getElementById("resultStats-" + stat)
    container.innerHTML = "Chargement en cours...";
    let data = await getStats(stat);
    container.innerHTML = "";
    switch (stat) {
        case "server":
            displayServerStat(data, container);
            break;
        case "age":
            displayAgeStat(data, container);
            break;
        case "scenario1":
            displayScenario1Stat(data, container);
            break;
        case "scenario2":
            displayScenario2Stat(data, container);
            break;
    }
}

async function getStats(stat){
    return await fetch("http://localhost:8080/" + stat, {
        method: "get",
        headers: {
            'Accept': 'application/json'
        },
    }).then(async response => {
        if (response.ok) {
            let data = await response.json();
            console.log("Data from response", data);
            return data;
        } else {
            console.log(response)
        }
    });
}

async function displayServerStat(data, container) {
    let count = 0
    for (const key in data.stats) {
        if (data.stats.hasOwnProperty(key)) {
            const html = `<p>${key} : ${data.stats[key]}</p>`;
            container.innerHTML += html;
            count += data.stats[key];
        }
    }
    document.getElementById("countStats-server").innerHTML = `<p>Nombre de sites trouvés avec le header "Server" : ${count}</p>`
}

async function displayAgeStat(data, container) {
    container.innerHTML += `<p>Moyenne de l'age : ${Math.round(data.average)} secondes`;
    container.innerHTML += `<p>Ecart type de l'age : ${Math.round(data.std_dev)} secondes`;
}

async function displayScenario1Stat(data, container) {
    container.innerHTML += `<p>Moyenne de l'age : ${Math.round(data.avg_age)} secondes</p>`;
    container.innerHTML += `<p>Ecart type de l'age : ${Math.round(data.std_dev_age)} secondes</p>`;
    container.innerHTML += `<p>Total de la longueur du contenu : ${data.total_length} octets (${Math.round(((data.total_length)/1000) * 100) / 100} Mo)</p>`;
    container.innerHTML += `<p>Moyenne de la longueur du contenu : ${data.avg_length} octets (${Math.round(((data.avg_length)/1000) * 100) / 100} Mo)</p>`;
    container.innerHTML += `<p>Ecart type de la longueur du contenu : ${Math.round(data.std_dev_length)} octets (${Math.round(((data.std_dev_length)/1000) * 100) / 100} Mo)</p>`;
}

async function displayScenario2Stat(data, container) {
    container.innerHTML += `<p>Total des Set-Cookie : ${data.total_cookie} valeurs enregistrées dans les cookies</p>`;
    container.innerHTML += `<p>Moyenne des Set-Cookie : ${Math.round(data.avg_cookie * 100) / 100} cookies</p>`;
    container.innerHTML += `<p>Ecart type des Set-Cookie : ${Math.round(data.std_dev_cookie)} cookies</p>`;
}