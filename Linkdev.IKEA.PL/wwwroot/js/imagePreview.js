const imageInput = document.getElementById("imageInput");

imageInput.addEventListener("change", (e) => {
    const imagePreview = document.getElementById("imagePreview");
    const url = URL.createObjectURL(event.target.files[0]);
    console.log(url);
    imagePreview.setAttribute("src", url);
})

const uploadBtn = document.getElementById("uploadButton");

uploadBtn.addEventListener("click", () => imageInput.click())