const salaryInput = document.getElementById("editableSalary");
const successAlert = document.getElementById("success-alert");
const errorAlert = document.getElementById("error-alert");
document.getElementById("updateSalaryButton").addEventListener("click", updateManager)


function getManagerId(){
    const path = window.location.pathname;
    managerId = path.substring(path.lastIndexOf("/") + 1);
    return parseInt(managerId)
}
async function updateManager(){
    const id  = getManagerId();
    console.log("Manager ID:", id);
    const data = {"Salary": parseFloat(salaryInput.value)}
    console.log("Data:", data);
    const response = await fetch("/api/DeveloperManagers/" + id, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });
    
    if (response.status === 204){
        successAlert.style.display = 'block'
        setTimeout(function () {
            successAlert.style.display = 'none'
        }, 3000)
    }else {
        errorAlert.style.display = 'block'
        setTimeout(function (){
            errorAlert.style.display = 'none'
        }, 3000)
    }
}