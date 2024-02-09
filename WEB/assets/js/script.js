let omtxt = document.getElementById("inputOM");
let nametxt = document.getElementById("inputName");
let pointtxt = document.getElementById("inputPoints");
let resTable = document.getElementById("results");
let resCSV = document.getElementById("csvOut");

function loadResults(data) {
    resTable.innerHTML = `
        <tr> 
            <th onclick="sortByKeyClick(allData, 'OM_Azonosito')" id="thOM_Azonosito">OM</th>
            <th onclick="sortByKeyClick(allData, 'Neve')" id="thNeve">Név</th>
            <th onclick="sortByKeyClick(allData, 'Matematika')" id="thMatematika">Matematika</th>
            <th onclick="sortByKeyClick(allData, 'Magyar')" id="thMagyar">Magyar</th>
            <th onclick="sortBySum(allData, 'Osszesen')" id="thOsszesen">Összesen</th>
        </tr>`;

    data.forEach(tanulo => {
        resTable.innerHTML += `
            <tr ondblclick="CSVLine(${tanulo.OM_Azonosito})">
                <td>${tanulo.OM_Azonosito}</td>
                <td>${tanulo.Neve}</td>
                <td>${tanulo.Matematika}</td>
                <td>${tanulo.Magyar}</td>
                <td>${tanulo.Matematika + tanulo.Magyar}</td>
            </tr>`;
    });
}

function showInputs() {
    let omcb = document.getElementById("inputOMCB").checked;
    let nevcb = document.getElementById("inputNameCB").checked;

    if (omcb) {
        omtxt.style.display = "inline-block"
    } else {
        omtxt.style.display = "none"
    }

    if (nevcb) {
        nametxt.style.display = "inline-block"
    } else {
        nametxt.style.display = "none"
    }
}

function checkFilters() {
    let omcb = document.getElementById("inputOMCB").checked;
    let nevcb = document.getElementById("inputNameCB").checked;
    let filteredList = [];

    if (omcb || omtxt.value.trim() && nevcb) {
        allData.forEach(tanulo => {
            if (tanulo.Neve.startsWith(nametxt) && tanulo.OM_Azonosito.startsWith(omtxt)) {
                filteredList.push(tanulo);
            }
        });
    }
    else if (omcb && omtxt.value.trim() != "") {
        allData.forEach(tanulo => {
            if (tanulo.OM_Azonosito.startsWith(omtxt.value)) {
                filteredList.push(tanulo);
            }
        })
    }
    else if (nevcb && nametxt.value.trim() != "") {
        allData.forEach(tanulo => {
            if (tanulo.Neve.startsWith(nametxt.value)) {
                filteredList.push(tanulo);
            }
        })
    }
    else if (omtxt.value.trim() == "" && nametxt.value.trim() == "") {
        filteredList = allData;
    }
    loadResults(filteredList);
}

function checkPoints() {
    if (pointtxt.value.trim() == "" || parseInt(pointtxt.value) < 0) {
        pointtxt.value = 0
        loadResults(allData);
        displayStats(allData);
    } else {
        let filteredList = [];

        allData.forEach(tanulo => {
            if (tanulo.Magyar + tanulo.Matematika >= parseInt(pointtxt.value)) {
                filteredList.push(tanulo);
            }
        });
        loadResults(filteredList);
        displayStats(filteredList);
    }
}

function displayStats(array) {
    let tanulokSzama = document.getElementById("tanulokSzama");
    let matekAtlag = document.getElementById("matekAtlag");
    let magyarAtlag = document.getElementById("magyarAtlag");
    let osszAtlag = document.getElementById("osszAtlag");

    let matSzum = 0;
    let magySzum = 0;
    let osszSzum = 0;

    array.forEach(tanulo => {
        matSzum += tanulo.Matematika;
        magySzum += tanulo.Magyar;
        osszSzum += (tanulo.Matematika + tanulo.Magyar);
    });

    console.log(array.length > 0);

    tanulokSzama.innerHTML = array.length;
    matekAtlag.innerHTML = array.length > 0 ? (matSzum / array.length).toFixed(1) : "0";
    magyarAtlag.innerHTML = array.length > 0 ? (magySzum / array.length).toFixed(1) : "0";
    osszAtlag.innerHTML = array.length > 0 ? (osszSzum / array.length).toFixed(1) : "0";
    resTable.style.display = array.length > 0 ? "table" : "none";
}

function CSVLine(om) {
    allData.forEach(tanulo => {
        if (tanulo.OM_Azonosito == om) {
            resCSV.innerHTML += `${tanulo.OM_Azonosito};${tanulo.Neve};${tanulo.ErtesitesiCime};${tanulo.Email};${tanulo.Matematika};${tanulo.Magyar}<br>`;
            resCSV.scrollIntoView();
        }
    });
}

function sortByKey(array, key) {
    return array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    });
}

function sortByKeyClick(array, key) {
    console.log(document.getElementById(`th${key}`));
    document.getElementById(`th${key}`).innerHTML += " ▲"
    loadResults(array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    }));
}

function sortBySum(array, key) {
    loadResults(array.sort(function (a, b) {
        var x = a['Matematika'] + a['Magyar']; var y = b['Matematika'] + b['Magyar'];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    }));
    document.getElementById(`th${key}`).innerHTML += " ▲"
}

sortByKey(allData, 'Neve');
loadResults(allData);
displayStats(allData);