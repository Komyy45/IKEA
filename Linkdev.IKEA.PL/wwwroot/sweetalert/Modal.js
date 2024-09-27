const deleteBtns = document.getElementsByName("Delete");

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
                
                if (willDelete) {
                    $.ajax(
                        {
                            url: `${location.pathname.split('/')[1]}/Delete`,
                            type: "POST",
                            data: { id: deleteBtn.id },
                            success: () => {
                                deleteBtn.parentElement.parentElement.remove();
                                swal(`Your ${location.pathname.split('/')[1]} has been deleted successfully!`, { icon: "success" });
                            },
                            error: () => {
                                swal("Error", "An Error has been occured while deleting!", { icon : "info"});
                            }
                        }
                    )
                } else {
                    swal(`Your ${location.pathname.split('/')[1]} is safe now!`, { icon: "success" });
                }
            });
}));