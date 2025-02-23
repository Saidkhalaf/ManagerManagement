const tableBody = document.getElementById('clientTableBody')
const refreshButton = document.getElementById('refreshButton')

async function retrieveClient() {
<<<<<<< HEAD
    const response = await fetch(`/api/Clients/`, {
=======
    const response = await fetch('api/Clients', {
>>>>>>> 972bd91c2bc7f107a7876d72e69174d2c19c7900
        body: "",
        headers: {
            Accept: "application/json"
        }
    });
    if (response.ok) {
        tableBody.innerHTML = ''; // clear table body
        /**
         * @type{[{id:number , Name: String , BirthDate: Date , Email: string}]}
         */

        const clients = await response.json();
        for (const client of clients) {
            tableBody.innerHTML += `  
            <tr>  
              <td>${client.Name}</td>  
              <td>${client.BirthDate}</td>  
              <td>${client.Email}</td> 
              <td><a href="Client/Details/${client.id}">Details</a></td> 
            </tr>
            `;
        }
    } else {
        console.error('Failed to retrieve clients');
    }
}

refreshButton.addEventListener('click', retrieveClient)
 
