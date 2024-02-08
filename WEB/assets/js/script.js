let omtxt = document.getElementById("inputOM");
let nametxt = document.getElementById("inputName");
let pointtxt = document.getElementById("inputPoints");

function loadResults(data) {
    let resTable = document.getElementById("results");
    resTable.innerHTML = `
        <tr> <th onclick="sortByKeyClick(allData, 'OM_Azonosito')">OM</th>
            <th onclick="sortByKeyClick(allData, 'Neve')">Név</th>
            <th onclick="sortByKeyClick(allData, 'Matematika')">Matek</th>
            <th onclick="sortByKeyClick(allData, 'Magyar')">Magyar</th>
            <th onclick="sortBySum(allData)">Összesen</th>
        </tr>`;

    data.forEach(tanulo => {
        resTable.innerHTML +=
            `
            <tr>
                <td>${tanulo.OM_Azonosito}</td>
                <td>${tanulo.Neve}</td>
                <td>${tanulo.Matematika}</td>
                <td>${tanulo.Magyar}</td>
                <td>${tanulo.Matematika + tanulo.Magyar}</td>
            </tr>
        `;
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
        console.log(osszSzum)
    });

    console.log(matSzum)
    console.log(magySzum)
    console.log(osszSzum)

    tanulokSzama.innerHTML = array.length;
    matekAtlag.innerHTML = (matSzum / array.length).toFixed(1);
    magyarAtlag.innerHTML = (magySzum / array.length).toFixed(1);
    osszAtlag.innerHTML = (osszSzum / array.length).toFixed(1);

}

function sortByKey(array, key) {
    return array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    });
}

function sortByKeyClick(array, key) {
    loadResults(array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    }));
}

function sortBySum(array) {
    loadResults(array.sort(function (a, b) {
        var x = a['Matematika'] + a['Magyar']; var y = b['Matematika'] + b['Magyar'];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    }));
}

sortByKey(allData, 'Neve');
loadResults(allData);
displayStats(allData);