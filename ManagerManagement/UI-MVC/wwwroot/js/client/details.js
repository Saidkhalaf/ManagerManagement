const managerTableBody = document.getElementById("managerTableBody")

const managerSelect = document.getElementById("managerSelect")

const addButton = document.getElementById('addButton')

const taskNameInput = document.getElementById('taskNameInput');



function getDeveloperId() {
    // Formaat: /api/iets?id=123 
    const urlSearchParams = new URLSearchParams(window.location.search);
    let developerId = urlSearchParams.get('id')
    if (!developerId) {
        const path = window.location.pathname
        developerId = path.substring(path.lastIndexOf("/") + 1)
    }

    return parseInt(developerId)
}

function getProjectId() {
    // Formaat: /api/iets?id=123 
    const urlSearchParams = new URLSearchParams(window.location.search);
    let projectId = urlSearchParams.get('id')
    if (!projectId) {
        const path = window.location.pathname
        projectId = path.substring(path.lastIndexOf("/") + 1)
    }

    return parseInt(projectId)
}

async function loadDeveloperTasksWithManagers(){
    const response = await fetch(`/api/Developers/${getDeveloperId()}/developerTasks`, {
        headers: {
            "Accept": "application/json"
        }
    });
    
    if (response.status === 200){
        const projects = await response.json();
        managerTableBody.innerHTML = '';
        for (const task of tasks){
            managerTableBody.innerHTML +=
                `<tr>
                    <td>${task.taskName}</td>
                    <td>${task.developerId}</td>
                 <tr>`
        }
    } else {
        clientTableBody.innerHTML +=
            ` <tr>
                   <td colspan="2">No tasks were found</td>
              </tr>`;
    }
}

async function loadAllManagers(){
    const response = await fetch(`/api/DeveloperManagers`, {
        headers: {
            Accept: "application/json"
        }
    })
    if (response.status === 200) {
        const managers = await response.json()
        /**
         * @type{[{managerId: number}]}
         */
        managerSelect.innerHTML = '';
        for (const manager of managers) {
            managerSelect.innerHTML += `
 <option value="${manager.id}">${managers.managerId}</option>`

        }
    }
}

async function addNewTask() {
    const taskName = taskNameInput.value;
    const response = await fetch('api/DeveloperTasks',
        {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "id": managerSelect.options[managerSelect.selectedIndex].value,
                "taskName": taskName,
                "developerId": getDeveloperId(),
                "projectId": getProjectId()
            })
        })
    if (response.status === 201) {
        loadDeveloperTasksWithManagers()
        document.getElementsByTagName('form')[0].reset()
    }
}

loadDeveloperTasksWithManagers()
loadAllManagers()

addButton.addEventListener('click', addNewTask)

