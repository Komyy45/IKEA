﻿const searchInput = document.getElementById("SearchInp");

searchInput.addEventListener("keyup", () => {
    const xhr = new XMLHttpRequest();

    xhr.open("GET", `/Employee/Search?searchValue=${searchInput.value}`);

    xhr.send();

    xhr.onreadystatechange = () => {

        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                const employeeList = document.getElementById("employeeList");
                employeeList.innerHTML = xhr.responseText;
            }
            else {
                swal("Error", "An Error has been occured while deleting!", { icon: "info" });
            }
        }

    }

})
