/* Assets */

jQueryAjaxPostCreateAsset = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            processData: false,
            contentType: false,
            success: function (response) {
                console.log("AJAX Success:", response);
                if (response.success) {
                    console.log("Asset with Asset ID " + response.data.assetid + " created successfully.");
                    alert("Asset record created successfully!");
                    console.log(response)
                    $('#createAssetForm').modal('hide');
                    window.location.href = '/AssetMgmt/AssetDetails?id=' + response.data.uniqueassetid;
                } else {
                    if (response.errors && response.errors.length > 0) {
                        var errorMessage = "<ul>";
                        response.errors.forEach(function (error) {
                            errorMessage += "<li>" + error + "</li>";
                        });
                        errorMessage += "</ul>";
                        $('#validationErrors').html(errorMessage);
                        $('#validationErrors').show();
                    } else {
                        console.error("Unknown error occurred.");
                        alert("Unknown error occurred.");
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("AJAX Error:", textStatus, errorThrown);
                console.error("Error creating asset:", errorThrown);
                alert("Error creating asset: " + errorThrown);
            }
        });
        return false;
    } catch (ex) {
        console.log(ex);
    }
}

jQueryAjaxPostUpdateAsset = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            processData: false,
            contentType: false,
            success: function (response) {
                console.log("AJAX Success:", response);
                if (response.success) {
                    console.log("Asset with Asset ID " + response.data.assetid + " updated successfully.");
                    alert("Asset record updated successfully!");
                    console.log(response)
                    $('#updateAssetForm').modal('hide');
                    location.reload()
                } else {

                    if (response.errors && response.errors.length > 0) {
                        var errorMessage = "<ul>";
                        response.errors.forEach(function (error) {
                            errorMessage += "<li>" + error + "</li>";
                        });
                        errorMessage += "</ul>";
                        $('#validationErrors').html(errorMessage);
                        $('#validationErrors').show();
                    } else {
                        console.error("Unknown error occurred.");
                        alert("Unknown error occurred.");
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("AJAX Error:", textStatus, errorThrown);

                console.error("Error creating asset:", errorThrown);
                alert("Error creating asset: " + errorThrown);
            }
        });
        return false;
    } catch (ex) {
        console.log(ex);
    }
}
jQueryAjaxPostDeleteAsset = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (response) {
                console.log("Asset with Asset ID " + response.assetid + " deleted successfully.");
                alert("Asset record deleted successfully!");
                $('#deleteAssetForm').modal('hide');
                window.location.href = response.url;
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }

    //prevent default form submit event
    return false;
}

/* Content */

jQueryAjaxPostCreateContent = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            processData: false,
            contentType: false,
            success: function (response) {
                console.log("AJAX Success:", response);
                if (response.success) {
                    //console.log("Content with Asset ID " + response.data.assetid + " created successfully.");
                    alert("Content record created successfully!");
                    console.log(response)
                    $('#createContentForm').modal('hide');
                    location.reload()
                } else {

                    if (response.errors && response.errors.length > 0) {
                        var errorMessage = "<ul>";
                        response.errors.forEach(function (error) {
                            errorMessage += "<li>" + error + "</li>";
                        });
                        errorMessage += "</ul>";
                        $('#validationErrors').html(errorMessage);
                        $('#validationErrors').show();
                    } else {
                        console.error("Unknown error occurred.");
                        alert("Unknown error occurred.");
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("AJAX Error:", textStatus, errorThrown);
                console.error("Error creating asset:", errorThrown);
                alert("Error creating asset: " + errorThrown);
            }
        });
        return false;
    } catch (ex) {
        console.log(ex);
    }
}


jQueryAjaxPostUpdateContent = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            processData: false,
            contentType: false,
            success: function (response) {
                console.log("AJAX Success:", response);
                if (response.success) {
                    alert("Content record updated successfully!");
                    console.log(response)
                    $('#updateContentForm').modal('hide');
                    location.reload()
                } else {
                    if (response.errors && response.errors.length > 0) {
                        var errorMessage = "<ul>";
                        response.errors.forEach(function (error) {
                            errorMessage += "<li>" + error + "</li>";
                        });
                        errorMessage += "</ul>";
                        $('#validationErrors').html(errorMessage);
                        $('#validationErrors').show();
                    } else {
                        console.error("Unknown error occurred.");
                        alert("Unknown error occurred.");
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("AJAX Error:", textStatus, errorThrown);
                console.error("Error creating asset:", errorThrown);
                alert("Error creating asset: " + errorThrown);
            }
        });
        return false;
    } catch (ex) {
        console.log(ex);
    }
}

jQueryAjaxPostDeleteContent = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                alert("Content record deleted successfully!");
                $('#deleteContentForm').modal('hide');
                location.reload()
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    //prevent default form submit event
    return false;
}

/* Distribution*/

jQueryAjaxPostCreateDistrib = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            processData: false,
            contentType: false,
            success: function (response) {
                console.log("AJAX Success:", response);
                if (response.success) {
                    alert("Distribution record created successfully!");
                    console.log(response)
                    $('#createDistribForm').modal('hide');
                    location.reload()
                } else {

                    if (response.errors && response.errors.length > 0) {
                        var errorMessage = "<ul>";
                        response.errors.forEach(function (error) {
                            errorMessage += "<li>" + error + "</li>";
                        });
                        errorMessage += "</ul>";
                        $('#validationErrors').html(errorMessage);
                        $('#validationErrors').show();
                    } else {
                        console.error("Unknown error occurred.");
                        alert("Unknown error occurred.");
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("AJAX Error:", textStatus, errorThrown);

                console.error("Error creating asset:", errorThrown);
                alert("Error creating asset: " + errorThrown);
            }
        });
        return false;
    } catch (ex) {
        console.log(ex);
    }
}
jQueryAjaxPostUpdateDistrib = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            processData: false,
            contentType: false,
            success: function (response) {
                console.log("AJAX Success:", response);
                if (response.success) {
                    alert("Distribution record updated successfully!");
                    console.log(response)
                    $('#updateDistribForm').modal('hide');
                    location.reload()
                } else {
                    if (response.errors && response.errors.length > 0) {
                        var errorMessage = "<ul>";
                        response.errors.forEach(function (error) {
                            errorMessage += "<li>" + error + "</li>";
                        });
                        errorMessage += "</ul>";
                        $('#validationErrors').html(errorMessage);
                        $('#validationErrors').show();
                    } else {
                        console.error("Unknown error occurred.");
                        alert("Unknown error occurred.");
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("AJAX Error:", textStatus, errorThrown);
                console.error("Error creating asset:", errorThrown);
                alert("Error creating asset: " + errorThrown);
            }
        });
        return false;
    } catch (ex) {
        console.log(ex);
    }
}

jQueryAjaxPostDeleteDistrib = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                alert("Distribution record deleted successfully!");
                $('#deleteDistribForm').modal('hide');
                location.reload()
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    //prevent default form submit event
    return false;
}