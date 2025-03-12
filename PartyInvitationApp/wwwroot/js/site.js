document.addEventListener("DOMContentLoaded", function () {
    console.log("JavaScript Loaded!");

    // Confirmation before deleting a party
    let deleteButtons = document.querySelectorAll(".btn-delete");
    deleteButtons.forEach(button => {
        button.addEventListener("click", function (event) {
            if (!confirm("Are you sure you want to delete this?")) {
                event.preventDefault(); // Stop action if user cancels
            }
        });
    });
});
