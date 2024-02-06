let omtxt = document.getElementById("inputOM");
let nametxt = document.getElementById("inputName");
let pointtxt = document.getElementById("inputPoints");

function loadResults(data) {
    let resTable = document.getElementById("results");
    resTable.innerHTML = `
        <tr> <th>OM</th>
            <th>Név</th>
            <th>Matek</th>
            <th>Magyar</th>
            <th>Összesen</th>
        </tr>`;

    sortByKey(data, 'Neve')
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

}

function checkPoints() {
    if (pointtxt.value.trim() == "" || parseInt(pointtxt.value) < 0) {
        pointtxt.value = 0
        loadResults(allData);
    } else {
        let filteredList = [];

        allData.forEach(tanulo => {
            if (tanulo.Magyar + tanulo.Matematika >= parseInt(pointtxt.value)) {
                filteredList.push(tanulo);
            }
        });
        loadResults(filteredList);
    }

}

function sortByKey(array, key) {
    return array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    });
}

loadResults(allData);