
/**
saveSingleData(url,data,method)
*/
function saveSingleData(_url,data,method)
{
 
    var result = 0;var message=""; var datalist="";

    if (typeof _url == 'undefined' && !_url) {
        result = 0;
        message = 'No Url found';
    }

    else{
        var methodType = typeof method === 'undefined'? 'GET':method=='POST'?method:'GET';
        var getData = typeof data === 'undefined' ? {} : data ? data : {};
   
        try {
            $.ajax({
                type: methodType,
                contentType: "application/json; charset=utf-8",
                url: _url,
                data : getData,
                dataType: 'json',
                async: false,
                success: function (data) {
                    result = data.result;
                    message = data.message;

                    if (typeof data.datalist != 'undefined') {
                        datalist = data.dataList;
                    }
                },
                error: function (request, status, error) {
                    result = 0;
                    message = error;
                }
            });

        }
        catch(err){
            result = 0;
            message = err;
            datalist = "";
        }
       
    }
    return {result:result,message:message,dataList:datalist};
   
}

function loadDropDown(_url, data, method, targetTag, selectedValue, AsyncMode) {

    var result = 0; var message = "";  var dataItem = "";
    $(targetTag).html("");
    if (typeof _url == 'undefined' && !_url) {
        result = 0;
        message = 'No Url found';
    }
    else if (typeof targetTag == 'undefined' && !targetTag) {
        result = 0;
        dataItem ='No Target tag found';
    }
    else {
        var methodType = typeof method === 'undefined' ? 'GET' : method == 'POST' ? method : 'GET';
        var getData = typeof data === 'undefined' ? {} : data ? data : {};
        var _AsyncMode = typeof AsyncMode === 'undefined' ? true : AsyncMode == true ? AsyncMode : false;
        var selectvar = 'selected';
        var Nonselectvar = '';
        try {
           
            $.ajax({
                type: methodType,
                contentType: "application/json; charset=utf-8",
                url: _url,
                data: getData,
                dataType: 'json',
                async: _AsyncMode,
                success: function (obj) {
                    result = obj.result;
                    console.log(obj.data);
                    if (typeof obj.data != 'undefined') {
                        $.each(obj.data, function (k, v) {
                            var selectedParameter = v.Value===selectedValue?selectvar:Nonselectvar;
                            dataItem = dataItem + '<option ' + selectedParameter + ' value="' + v.Value + '">' + v.Text + '</option>';
                        });
                    }
                  
                },
                error: function (request, status, error) {
                    dataItem="";
                    result = 0;
                    message = error;
                 
                }
            });

        }
        catch (err) {
            result = 0;
            message = err;
            datalist = "";
            dataItem = err;
            alert(err);
        }

    }
    $(targetTag).html(dataItem);
  
    

}


function getDropDownList(_url, data, method, targetTag, selectedValue, AsyncMode) {
    var result = 0; var message = "";   var dataItem = "";
    $(targetTag).html("");
    if (typeof _url == 'undefined' && !_url) {
        result = 0;
        message = 'No Url found';
    }
    else if (typeof targetTag == 'undefined' && !targetTag) {
        result = 0;
        dataItem = 'No Target tag found';
    }
    else {
        var methodType = typeof method === 'undefined' ? 'GET' : method == 'POST' ? method : 'GET';
        var getData = typeof data === 'undefined' ? {} : data ? data : {};
        var _AsyncMode = typeof AsyncMode === 'undefined' ? true : AsyncMode == true ? AsyncMode : false;
        var selectvar = 'selected';
        var Nonselectvar = '';

        try {
         
            $.ajax({
                type: methodType,
                contentType: "application/json; charset=utf-8",
                url: _url,
                data: getData,
                dataType: 'json',
                async: _AsyncMode,
                success: function (obj) {
                    result = obj.result;
                    if (typeof obj.data != 'undefined') {
                        $.each(obj.data, function (k, v) {
                            var selectedParameter = v.Value === selectedValue ? selectvar : Nonselectvar;
                            dataItem = dataItem + '<option ' + selectedParameter + ' value="' + v.Value + '">' + v.Text + '</option>';
                        });
                    }
                },
                error: function (request, status, error) {
                    dataItem="";
                    result = 0;
                    message = error;
                }
            });

        }
        catch (err) {
            result = 0;
            message = err;
            datalist = "";
            dataItem=err;
        }

    }
    return dataItem;
    

}

function ShowReport(url, data) {
    var nUrl = url+"?"+data;
    // window.open(nUrl, 'mywindow', 'fullscreen=yes, scrollbars=auto');
    window.open(nUrl);
}

function AutoCompleteLoad(autocompleteField, _url, hiddenField, _data) {
    var extraData = "";
    if (typeof _data == 'undefined' && !_data) {
        extraData = "";
    } else {
        extraData = _data;
    }
    $(autocompleteField).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _url,
                data: { Prefix: request.term, extraData },
                dataType: 'json',
                type: 'get',
                contentType: 'application/json;chaset=utf-8',
                success: function (data) {
                    response($.map(data.data, function (item) {
                    
                        return { label: item.label, value: item.label, hiddenId: item.value };
                    }));
                },
                error: function (response) {
                    $(hiddenField).val("");
                    $.alert('Error', response.responseText);
                },
                failure: function (response) {
                    $(hiddenField).val("");
                    $.alert('Error', response.responseText);
                }
                
            });
        },
        select: function (e, i) {
            $(hiddenField).val(i.item.hiddenId);
        },
        minLength: 1,
        messages: {
            noResults: 'Not Found'
        }
    });
}

function getDataList(_url, data, method, AsyncMode) {

    var result = 0; var message = ""; var returnList = "";
    if (typeof _url == 'undefined' && !_url) {
        result = 0;
        message = 'No Url found';
    }
    else {
        var methodType = typeof method === 'undefined' ? 'GET' : method == 'POST' ? method : 'GET';
        var getData = typeof data === 'undefined' ? {} : data ? data : {};
        var _AsyncMode = typeof AsyncMode === 'undefined' ? true : AsyncMode == true ? AsyncMode : false;
        try {

            $.ajax({
                type: methodType,
                contentType: "application/json; charset=utf-8",
                url: _url,
                data: getData,
                dataType: 'json',
                async: _AsyncMode,
                success: function (obj) {
                    result = obj.result;
                    if (typeof obj.data != 'undefined') {
                        returnList = obj.data;
                    }

                },
                error: function (request, status, error) {
                    returnList = "";
                    result = 0;
                    message = error;

                }
            });

        }
        catch (err) {
            result = 0;
            message = err;
            returnList = "";
            alert(err);
         
        }

    }
    return { result:result,data:returnList,message:message };



}