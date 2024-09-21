const deleteBtns = document.querySelectorAll("#Delete");

deleteBtns.forEach(deleteBtn =>
    deleteBtn.addEventListener("click", () => {

        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this imaginary file!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                console.log(location.pathname);
                if (willDelete) {
                    $.ajax(
                        {
                            url: `${location.pathname}/Delete`,
                            type: "Post",
                            data: { id: deleteBtn.getAttribute("data") },
                            success: () => {
                                location.reload();
                            },
                            error: () => {
                                swal("Error", "An Error has been occured while deleting!", "Error");
                            }
                        }
                    )
                } else {
                    swal("Your department is safe!", { icon: "success" });
                }
            });
    }));