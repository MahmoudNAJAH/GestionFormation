
//Quand on appelle l'action Index du controller
//On veut appliquer les différentes function js pour que ce soit plus jolie
window.onload = function () {
    RefreshDataGrid();
}

//Applique les 3 fonctions js ppour que ce soit plus jolie
function RefreshDataGrid() {
    GetDotDotDot(); //Pour les ... quand le texte est trop grand sur plusieurs lignes (dans le cas de nom de fichiers trop long par exemple)
    GetImageSrc();  //Pour avoir une image correspondant à l'extension du fichier
    DisplayDeleteButton();  //Pour afficher/cacher les croix rouges pour supprimer les fichiers
}

//On détecte le changement de sélection dans la combo box
//Pour afficher le datagrid
$(document).ready(function () {
    $("#SessionCursus").change(function () {
        if ($("#SessionCursus").val() != "") SessionCursus();
        else $("#Details").html("");
    });
});

//Effectué quand on choisi une session de cursus 
function SessionCursus() {
    const obj = {
        dirPath: $("#SessionCursus").val(),
    }
    GetDetails(obj);
};

//Effectué quand on clique sur un dosier pour s'y déplacer
function GoInDirectory(param) {
    const obj = {
        dirpath: param,
    }
    GetDetails(obj);
};

//Fonction qui appelle l'action Detail dans le controller
//Quand on veut refresh le datagrid sans ajout de données
//  => quand on change de dossier ou de session de cursus 
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

//Utilisé pour retourner dans le dossier parent
//Appelle l'action PreviousFolder dans le controller
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

//Utilisé lors de na création d'un nouveau dossier
//Appelle l'action CreateDirectory du controller
function CreateDirectory() {
    const obj = {
        //On récupère les valeur des inputs
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

//Pour effacer un dossier et son contenu
//Appelle l'action DeleteDirectory du controller
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

//Utilisé pour supprimer un fichier
//Appelle l'action DeleteFile du controller
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

//Permet de donner aux fichier l'image correspondant à leur extension
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
            default:
                img.src = "/Images/Img_PartageFichier/file.png";
        }
    });
}

//Pour les icone de dossier/fichier, quand le texe est trop long, remplace ce qui dépasse par ...
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

//Pour afficher/cacher les croix de suppression de fichier/dossier
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


