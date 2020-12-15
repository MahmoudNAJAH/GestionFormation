
$(document).ready(function () {
    $("#SessionCursus").change(function () {
        if ($("#SessionCursus").val() != "") SessionCursus();
        else $("#Details").html("");
    });
});

function SessionCursus() {
    const obj = {
        dirPath: $("#SessionCursus").val(),
    }

    GetDetails(obj);
};

function GoInDirectory(param) {
    const obj = {
        dirpath: param,
    }

    GetDetails(obj);
};

function GetDetails(obj) {
    const options = {
        method: 'POST',
        body: JSON.stringify(obj),
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    fetch("/PartageFichier/Details", options)
        .then(function (response) {
            // The API call was successful!
            return response.text();
        }).then(function (html) {
            $("#Details").html(html);
            return $("#Details");
        }).then(function (html) {
            RefreshDataGrid();
        })
        .catch(function (err) {
            // There was an error
            console.warn('Something went wrong.', err);
        });
}

function PreviousFolder(param) {
    const obj = {
        dirPath: param,
    }

    const options = {
        method: 'POST',
        body: JSON.stringify(obj),
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    fetch("/PartageFichier/PreviousFolder", options)
        .then(function (response) {
            // The API call was successful!
            return response.text();
        }).then(function (html) {
            $("#Details").html(html);
            return $("#Details");
        }).then(function (html) {
            RefreshDataGrid();
        }).catch(function (err) {
            // There was an error
            console.warn('Something went wrong.', err);
        });
}

function CreateDirectory() {
    const obj = {
        dirPath: document.getElementById('createDirectory-dirPath').value,
        dirName: document.getElementById('createDirectory-dirName').value,
    }

    const options = {
        method: 'POST',
        body: JSON.stringify(obj),
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    fetch("/PartageFichier/CreateDirectory", options)
        .then(function (response) {
            // The API call was successful!
            return response.text();
        }).then(function (html) {
            $("#Details").html(html);
            return $("#Details");
        }).then(function (html) {
            RefreshDataGrid();
        }).catch(function (err) {
            // There was an error
            console.warn('Something went wrong.', err);
        });
}

function CreateFile() {
    "use strict";
    const obj = {
        dirPath: document.getElementById('createFile-dirPath').value,
        //myFile: document.getElementById('createFile-myFile').files,
        myFile: $("#createFile-myFile").get(0).files,
    }

    const options = {
        method: 'POST',
        body: JSON.stringify(obj),
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    fetch("/PartageFichier/CreateFile", options)
        .then(function (response) {
            // The API call was successful!
            return response.text();
        }).then(function (html) {
            $("#Details").html(html);
            return $("#Details");
        }).then(function (html) {
            RefreshDataGrid();
        }).catch(function (err) {
            // There was an error
            console.warn('Something went wrong.', err);
        });
}



function DeleteDirectory(paramDirPath, paramDirName) {
    const obj = {
        dirPath: paramDirPath,
        dirName: paramDirName,
    }

    const options = {
        method: 'POST',
        body: JSON.stringify(obj),
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    fetch("/PartageFichier/DeleteDirectory", options)
        .then(function (response) {
            // The API call was successful!
            return response.text();
        }).then(function (html) {
            $("#Details").html(html);
            return $("#Details");
        }).then(function (html) {
            GetDotDotDot();
            GetImageSrc();
        }).catch(function (err) {
            // There was an error
            console.warn('Something went wrong.', err);
        });
}

function DeleteFile(paramDirPath, paramFileName) {
    const obj = {
        dirPath: paramDirPath,
        fileName: paramFileName,
    }

    const options = {
        method: 'POST',
        body: JSON.stringify(obj),
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    fetch("/PartageFichier/DeleteFile", options)
        .then(function (response) {
            // The API call was successful!
            return response.text();
        }).then(function (html) {
            $("#Details").html(html);
            return $("#Details");
        }).then(function (html) {
            GetDotDotDot();
            GetImageSrc();
        }).catch(function (err) {
            // There was an error
            console.warn('Something went wrong.', err);
        });
}

function GetImageSrc() {
    let images = document.querySelectorAll(".icon-partageFichier-file");
    images.forEach(function (img) {
        switch (img.name) {
            case 'txt':
                img.src = "/Images/Img_PartageFichier/txt.png";
                break;
            case 'pdf':
                img.src = "/Images/Img_PartageFichier/pdf.png";
                break;
            case 'csv':
                img.src = "/Images/Img_PartageFichier/csv.png";
                break;
            default:
                img.src = "/Images/Img_PartageFichier/file.png";
        }
    });
}

function GetDotDotDot() {
    let wrapper = document.querySelectorAll(".partage-fichier-icon-texte");
    wrapper.forEach(function (w) {
        let options = {
            callback: function (isTruncated) { },
            ellipsis: "\u2026 ",
            height: null,
            keep: null,
            tolerance: 0,
            truncate: "letter",
            watch: "window",
        };
        try {
            new Dotdotdot(w, options);
        }
        catch{
            console.error(w.innerHTML);
        }
    });
}

function DisplayDeleteButton() {
    let wrapper = document.querySelectorAll(".icon-partageFichier-link-croix");
    wrapper.forEach(function (w) {
        if (w.style.visibility == 'hidden') {
            w.style.visibility = 'visible';
        }
        else {
            w.style.visibility = 'hidden';
        }
        
    });
}

function RefreshDataGrid() {
    GetDotDotDot();
    GetImageSrc();
    DisplayDeleteButton();
}

window.onload = function () {
    RefreshDataGrid();
}
