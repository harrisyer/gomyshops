

var rootpath = '@Url.Content("~/")';

function GetPath(url) {
    return rootpath + url;
}


//init botton's icons
$.fn.InitBottonIcon = function () {
    $('.iconNew').addClass(CommonIconsTypes.New);
    $('.iconSave').addClass(CommonIconsTypes.Save);
    $('.iconReset').addClass(CommonIconsTypes.Reset);
    $('.iconSearch').addClass(CommonIconsTypes.Search);
    $('.iconBack').addClass(CommonIconsTypes.Back);
    $('.iconEdit').addClass(CommonIconsTypes.Edit);
    $('.iconRequired').addClass(CommonIconsTypes.Required);
    $('.iconCalendar').addClass(CommonIconsTypes.Calendar);
};
$(this).InitBottonIcon();

function InitBottonIcons()
{
    $(this).InitBottonIcon();
};

////$.validator.addMethod('validUrl', function (value, element) {
////    var url = $.validator.methods.url.bind(this);
////    return url(value, element) || url('http://' + value, element);
////}, 'Please enter a valid URL');

//for Date
//$('.routeDateControl').datetimepicker({
//    viewMode: 'days',
//    sideBySide: true,
//    format: CommonVariable.standardDateFormat(),
//    ignoreReadonly: true

//});

$.fn.tempusDominusCalender = function () {
    $('.routeDateControl').tempusDominus({
        allowInputToggle: true,
        // defaultDate: moment().format(),
        display: {
            sideBySide: false,
            viewMode: 'calendar',
            components: {
                decades: true,
                year: true,
                month: true,
                date: true,
                hours: false,
                minutes: false,
                seconds: false
            }
        },
        localization: {
            locale: 'en-gb',
            clear: 'Clear selection',
            close: 'Close the picker',
            format: CommonVariable.standardDateFormat()
        }
    });
};
$(this).tempusDominusCalender();
function InitDominusCalender() {
    $(this).tempusDominusCalender();
};




//


$(document).ready(function () {

   var $table = $('#tbllist'),
        $remove = $('#Deactivate'),
        selections = [];
    $remove.prop('disabled', true);

    $(document).on("click", "#Deactivate", function () {
        //$remove.click(function () {
        event.preventDefault();
        var $confirm = $("#edit-modal");
        $confirm.modal('show');
        $("#ConfirmSave").off('click').click(function () {
            $confirm.modal("hide");
            var y1 = JSON.stringify($table.bootstrapTable('getSelections'));
            //alert(y1);
            //alert($table.bootstrapTable('getSelections'));

            jsonObj = [];
            $($table.bootstrapTable('getSelections')).each(function (i, row) {
                row.DetailJson = null;
                row.EditJson = null;
                jsonObj.push(row);
            });
            //alert(JSON.stringify(jsonObj));

            $remove.prop('disabled', true);
            $.ajax({
                type: "post",
                url: GetPath(CommonVariable.modelName() + "/Deactived"),//GetPath(DeactivedUrl),
                dataType: "json",
                //data: { model: 'y1' },
                data: JSON.stringify(jsonObj),
                contentType: "application/json",
                success: function (result) {
                    if (result.Errors) {
                        showBoostrapAlert(result.Errors, "danger", true, true, false);
                    }
                    else {
                        //showBoostrapAlert(result.success, "success", true, true, true);
                        $table.bootstrapTable('refresh');
                    }//end if-else
                }//success
                ,
                error: function (xhr, status, error) {
                    //alert(error);
                    showBoostrapAlert("Please contact Admin", "danger", true, true, true);
                }
            });//ajax 

        });
        $("#btnNoConfirmYesNo").off('click').click(function () {
            $confirm.modal("hide");
            return false;
        });
    });//end $remove.click

    $table.on('check.bs.table uncheck.bs.table ' +
        'check-all.bs.table uncheck-all.bs.table', function () {
            $remove.prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

    $('#tbllist').on('load-success.bs.table', function () {
        $('#tbllist tr').each(function (rowid) {
            if (rowid !== 0) {
                var currentR$ = $(this);
                var currCheckBox$ = currentR$.find('td:first-child input');
                var currStatus$ = currentR$.find('td.StatusCol');

                if (currStatus$.length !== 0 && (currStatus$.text() !== "Pending" && currStatus$.text() !== "Active")) {
                    currCheckBox$.hide();
                }
            }
        });

    })


});
//Show Alert on top
function showBoostrapAlert(message, style, dismissable, empty, autoclose) {

    if (empty)
        $("#AlertPartial").empty();

    var html = "";
    var dismissableClass = dismissable ? "alert-dismissable" : null;
    var autoDisappearClass = autoclose ? "alert-disappear" : null;
    html = "<div class='alert alert-" + style + " " + dismissableClass + " " + autoDisappearClass + "' >";



    if (dismissable) {
        html = html + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>";


    }
    html = html + message + "</div>";
    $("#AlertPartial").append(html);
    $("#AlertPartial").show();


    $(".alert-disappear").delay(8000).fadeOut(300);

};

function showBoostrapAlertUI(TempData, message, style, dismissable, empty, autoclose) {

    if (empty)
        $("#AlertPartial").empty();

    //alert(TempData);

    var html = "";
    var dismissableClass = dismissable ? "alert-dismissable" : null;
    var autoDisappearClass = autoclose ? "alert-disappear" : null;
    html = "<div class='alert alert-" + style + " " + dismissableClass + " " + autoDisappearClass + "' >";



    if (dismissable) {
        html = html + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>";


    }
    html = html + message + "</div>";
    $("#AlertPartial").append(html);
    $("#AlertPartial").show();


    $(".alert-disappear").delay(8000).fadeOut(300);

};

//Show Alert on top
function showAlert(message) {
    //alert(message);
    var $confirm = $("#info-modal");
    var $modalbody = $confirm.find('.modal-body-text');
    $modalbody.html(message);
   
    jQuery.noConflict(); 
    $confirm.modal('show');
}

$("#edit-modal").css({
    top: ($(window).height() - $(this).height()) / 2,
    left: ($(window).width() - $(this).width()) / 2
});

$("#info-modal").css({
    top: ($(window).height() - 300) / 2,
    left: ($(window).width() - 500) / 2
});

//pop up modal
$('#body a').click(function (event) {
    var self = $(this);
    if (self.hasClass('linkbutton')) {
        event.preventDefault();

        var $confirm = $("#edit-modal");
        $confirm.modal('show');

        $("#ConfirmSave").off('click').click(function () {

            $confirm.modal("hide");
            window.location = self.attr('href');

        });
        $("#btnNoConfirmYesNo").off('click').click(function () {
            $confirm.modal("hide");
            return false;
        });
    }//end if
})

function popupConfirm() {
    var $confirm = $("#edit-modal");
    $confirm.modal('show');

    $("#ConfirmSave").off('click').click(function () {

        $confirm.modal("hide");
        window.location = self.attr('href');

    });
    $("#btnNoConfirmYesNo").off('click').click(function () {
        $confirm.modal("hide");
        return false;
    });
}

//for Listing
function TableListrowStyle(row, index) {
    if (index % 2 === 0) {
        return {
            classes: 'active'
        };
    }
    return {};
}

function operateCurrentStatusFormatter(value, row, index) {
    var html = $("<div />");
    //alert(row.CurrentStatus);
    if (row.CurrentStatus === 'C') {
        var a = $('<span/>', {
            class: 'CreationCss',
            text: value
        });

        html.append(a);
    }//end if
    if (row.CurrentStatus === 'C1') {
        var a = $('<span/>', {
            class: 'Creation1Css',
            text: value
        });

        html.append(a);
    }//end if
    if (row.CurrentStatus === 'E') {
        var a = $('<span/>', {
            class: 'EditCss',
            text: value
        });

        html.append(a);
    }//end if
    if (row.CurrentStatus === 'E1') {
        var a = $('<span/>', {
            class: 'Edit1Css',
            text: value
        });

        html.append(a);
    }//end if

    return html.clone().html();

}

function FormatterStatus(value, row, index) {
    if (value === '0') {
        return 'Deactivated';
    }
    if (value === '1') {
        return 'Active';
    }
    if (value === '3') {
        return 'Pending';
    }
    if (value === '4') {
        return 'Cancelled';
    }
    if (value === '2') {
        return 'Success';
    }
    if (value === '7') {
        return 'Suspended';
    }
    if (value === '8') {
        return 'Terminated';
    }
}

function operateMerchantFormatter(value, row, index) {

    if (row.appStatus === "2" || row.ActionListUserType === "C" || row.ActionListUserType === "P" || row.ActionListUserType === "A") {
        return [
            '<a class="listDetailsLink" href="javascript:void(0)" title="Details"><i class="fa fa-file-text-o" aria-hidden="true"></i>',
            '</a>  '
        ].join('');
    }
    else {
        return [
            '<a class="listDetailsLink" href="javascript:void(0)" title="Details"><i class="fa fa-file-text-o" aria-hidden="true"></i>',
            '</a>  ',
            '<a class="listEditLink" href="javascript:void(0)" title="Edit"><i class="fa fa-pencil-square-o" aria-hidden="true"></i>',
            '</a>'
        ].join('');
    }

}

function operateDetailOnlyFormatter(value, row, index) {
    return [
        '<a class="listDetailsLink" href="javascript:void(0)" title="Details"><i class="fa fa-file-text-o" aria-hidden="true"></i>',
        '</a>  '
    ].join('');
}

function FormatterDate(value, row, index) {
    return new Date(value);
};

function FormatterActionLink(value, row, index) {
    var html = $("<div />");
    $.each(value, function (idx, obj) {
        if (idx === 0) {
            var a = $('<a/>', {
                className: 'ListActionLink',
                href: obj
            });
            $(a).text("Details");
            html.append(a);
        }//end if
        if (idx === 1) {
            var b = $('<a/>', {
                className: 'ListActionLink',
                href: obj
            });
            $(b).text("Edit");
            html.append(b);
        }//end if
    });
    return html.clone().html();

}

function operateFormatter(value, row, index) {
    return [
        '<a class="listDetailsLink" href="javascript:void(0)" title="Details"><i class="fa-solid fa-file-lines fa-lg"></i>',
        '</a>  ',
        '<a class="listEditLink" href="javascript:void(0)" title="Edit"><i class="fa-solid fa-pen-to-square fa-lg"></i>"',
        '</a>'
    ].join('');
}

function operateTransactionRecordDetailsFormatter(value, row, index) {
    return [
        '<a class="listTransactionRecordDetailsLink" href="javascript:void(0)" title="Details">',
        value,
        '</a>  '
    ].join('');
}

function operateTransactionRecordListFormatter(value, row, index) {
    return [
        '<a class="listTransactionRecordListLink" href="javascript:void(0)" title="TransactionList">',
        value,
        '</a>  '
    ].join('');
}

function operateSuccessTransactionRecordListFormatter(value, row, index) {
    return [
        '<a class="listSuccessTransactionRecordListLink" href="javascript:void(0)" title="SucessTransactionList">',
        value,
        '</a>  '
    ].join('');
}

function operateResolvedTransactionRecordListFormatter(value, row, index) {
    return [
        '<a class="listResolvedTransactionRecordListLink" href="javascript:void(0)" title="ResolvedTransactionList">',
        value,
        '</a>  '
    ].join('');
}

function FormatterDecimal2Place(value, row, index) {

    return parseFloat(Math.round(value * 100) / 100).toFixed(2);

}

//$(".alert-disappear").delay(8000).fadeOut(300);

//window.setTimeout(function () {
//    $('.alert-disappear').fadeOut(300);
//}, 8000);

function ShowAlertsMessage()
{
    $(".AlertContainer").load(GetPath("Alerts/showAlert")).delay().queue(function () {

        window.setTimeout(function () {
            $('.alert-disappear').fadeOut(300);

        }, 8000);
        $('.AlertContainer').dequeue();
        //setTimeout(function () { $body.dequeue(); }, 2000);
        //$(".alert-disappear").delay(8000).fadeOut(300);
    });

}

function ShowAlertsMessage(classname, showAtDetail) {
    $.ajax({
        type: "POST",
        url: GetPath("Alerts/showAlert"),
        //data: JSON.stringify(myObject),
        //data: $(this).closest('form').serialize(),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (result) {
            if (!isNull(result)) {
                //alert(result);
                if (showAtDetail) {
                    $(classname).html(result).delay().queue(function () {
                        window.setTimeout(function () {
                            $('.alert-disappear').fadeOut(300);

                        }, 8000);
                        $(classname).dequeue();
                    });
                    //$(".AlertContainer").empty();
                }
                else
                {
                    $(".AlertContainer").html(result).delay().queue(function () {
                        window.setTimeout(function () {
                            $('.alert-disappear').fadeOut(300);

                        }, 8000);
                        $(".AlertContainer").dequeue();
                    });
                }

                

            }//end if
        },
        error: function (textStatus, errorThrown) {
            Success = false;
        }
    }).done(function (data) {

    });


    //$(".AlertContainer").load(GetPath("Alerts/showAlert")).delay().queue(function () {
    //    if (showAtDetail)
    //    {
    //    $(classname).html($('.AlertContainer').html());
    //    }
    //    window.setTimeout(function () {
    //        $('.alert-disappear').fadeOut(300);

    //    }, 8000);
    //    $('.AlertContainer').dequeue();
    //    //setTimeout(function () { $body.dequeue(); }, 2000);
    //    //$(".alert-disappear").delay(8000).fadeOut(300);
    //});


    //if (showAtDetail)
    //{
    //    $(classname).load(GetPath("Alerts/showAlert")).delay().queue(function () {

    //        window.setTimeout(function () {
    //            $('.alert-disappear').fadeOut(300);

    //        }, 8000);
    //        $(classname).dequeue();
    //        //setTimeout(function () { $body.dequeue(); }, 2000);
    //        //$(".alert-disappear").delay(8000).fadeOut(300);
    //    });
    //}


}


//for insert update details list   


window.operateEvents = {
    'click .listDetailsLink': function (e, value, row, index) {
        itemDetails(row.DetailJson);
    },
    'click .listEditLink': function (e, value, row, index) {
        itemEdit(row.EditJson);
    }
};

window.operateNewWindowEvents = {
    'click .listDetailsLink': function (e, value, row, index) {
        itemNewWindowDetails(row.DetailJson);
    }
};


window.operateTransactionRecordDetailsEvents = {
    'click .listTransactionRecordDetailsLink': function (e, value, row, index) {
        itemTransactionRecordDetails(row.TRDetailsJSON);
    }
};

window.operateTransactionRecordListEvents = {
    'click .listTransactionRecordListLink': function (e, value, row, index) {
        itemTransactionRecordList(row.TRListJSON);
    },
    'click .listSuccessTransactionRecordListLink': function (e, value, row, index) {
        itemTransactionRecordList(row.SuccessTRListJSON);
    },
    'click .listResolvedTransactionRecordListLink': function (e, value, row, index) {
        itemTransactionRecordList(row.ResolvedTRListJSON);
    }
};

$(document).on("click", ".itemEdit", function (e) {

    e.preventDefault();

    var aaa = $(this).data("editjson");

    //{"id":"ccc","id2":"","id3":"","id4":"","id5":""}
    //alert(JSON.stringify(aaa));
    itemEdit(aaa);

    return false;
});

$(document).on("click", "#btnFilter", function (e) {
    e.preventDefault();     
    listSearch();
});

$(document).on("click", ".itemNew", function (e) {
    e.preventDefault();
    itemCreate(e);
    return false;
});

$(document).on("click", ".itemUploadNew", function (e) {
    e.preventDefault();
    itemUploadCreate(e);
    return false;
});

$(document).on("click", ".itemSave", function (e) {
    e.preventDefault();
    $.ajax({
        type: "POST",
        url: GetPath(CommonVariable.modelName() + "/Create"),
        data: $(this).closest('form').serialize(),
        dataType: "html",
        success: function (result) {
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
            Success = false;
        }
    }).done(function (data) {
        if (!isNull(data)) {
            $('.itemCreateContainer').html(data);
            $('.itemDetailsContainer').hide();
            $('.itemEditContainer').hide();
            $('.itemCreateContainer').show();
            $('.itemsListingContainer').hide();
            if (!isNull(data.Errors)) {
                $('.itemCreateValidationMessageContainer').html(data.Errors);
            }
            if (!isNull(data.Success)) {
                $('.itemCreateMessageContainer').html(data.Success);
            }
            ShowAlertsMessage('.itemCreateMessageContainer', true);

            $('.routeDateControl').datetimepicker({
                viewMode: 'days',
                format: CommonVariable.standardDateFormat(),
                ignoreReadonly: true,
                keepOpen: false
            });
            var dateNow = new Date();
            $('.routeTimeControl').datetimepicker({
                format: 'LT',
                defaultDate: moment(dateNow).hours(0).minutes(0).seconds(0).milliseconds(0),
                ignoreReadonly: true
            });
            $('.routeDateDayControl').datetimepicker({
                viewMode: 'days',
                format: CommonVariable.standardDateFormat(),
                ignoreReadonly: true,
                keepOpen: false
            });

            var start1_date = $(".routeDateDayControl").data('DateTimePicker');
            var datePickerEnd = moment().startOf('day');//.subtract(1, 'days')
            if (typeof start1_date !== 'undefined' && start1_date) {
                start1_date.maxDate(datePickerEnd);
            }


            $('.routeDateYearControl').datetimepicker({
                viewMode: 'years',
                format: CommonVariable.standardDateFormat(),
                ignoreReadonly: true
            });

            var start_date = $(".routeDateYearControl").data('DateTimePicker');
            datePickerEnd = moment().startOf('day').subtract(1, 'days');
            if (typeof start_date !== 'undefined' && start_date) {
                start_date.maxDate(datePickerEnd);
            }
            
            SetReserveOtherFieldsVisibility();

            $(document).on("change", "#NewIsReserve", function (e) {
                SetReserveOtherFieldsVisibility();
            });
            
            //For Mid
            $('#IsRefund').bootstrapToggle();
            $('#IsChargeback').bootstrapToggle();
            $('#IsEmailToMerchant').bootstrapToggle();
            $(".TXPaymentContainer").addClass("active");
            $(".WeekPaymentContainer").removeClass("active");
            $('#IsReserve').bootstrapToggle();
            $('#PendingIsReserve').bootstrapToggle();
            $('#NewIsReserve').bootstrapToggle();

            $('#OnlyAllowPriorityBin').bootstrapToggle();

            validationPartialView('.itemCreateContainer');

        }
    });
    return false;
});

function SetReserveStartDateInputVisibility() {
    if ($('#NewIsReserve').prop("checked") === true && $('#IsReserve').prop("checked") === false)
    {
        $('#NewReserveStartDateInputSection').show();

        $('.routeDateDayControlReserveStartDate').datetimepicker({
            viewMode: 'days',
            format: CommonVariable.standardDateFormat(),
            ignoreReadonly: true,
            keepOpen: false
        });

        //Reserve Start Date Can Only Be Tomorrow Onwards
        var start1_date = $(".routeDateDayControlReserveStartDate").data('DateTimePicker');
        var datePickerEnd = moment().startOf('day').add(1, 'days');//.subtract(1, 'days')
        if (typeof start1_date !== 'undefined' && start1_date) {
            start1_date.minDate(datePickerEnd);

            if ($('#sNewReserveStartDate').val() === '') {
                //start1_date.Date = datePickerEnd;
                $('#sNewReserveStartDate').val(moment(datePickerEnd).format(CommonVariable.standardDateFormat()));
            }
            else {
                start1_date.Date = datePickerEnd;
            }
        }
    }
            else
    {
        $('#sNewReserveStartDate').val('');
        $('#NewReserveStartDateInputSection').hide();
    }
}

function SetReserveOtherFieldsVisibility() {
    if ($('#NewIsReserve').prop("checked") === true) {
        $('.reserveOtherFields').show();
    }
    else {
        $('#NewReserveFundingPeriod').val('');
        $('#NewReservePeriod').val('');
        $('#NewProcessorReservePercentage').val('');
        $('#NewInternalReservePercentage').val('');
        $('#sNewReserveStartDate').val('');
        $('.reserveOtherFields').hide();
    }
}

$(document).on("click", ".itemEditSave", function (e) {
    e.preventDefault();
    // var a = { 'abc': $(this).closest('form').serialize() };
    // var b = JSON.parse(JSON.stringify($(this).closest('form').serialize()));
    // alert($(this).closest('form').serialize());

    //For MID Funding, disabled inputs will have null values in model
    //Note : PaymentType will never be able to be edited, only PendingPaymentType can be
    $("#PaymentType").removeAttr("disabled");
    $("#WeekPaymentType").removeAttr("disabled");
    $('#IsReserve').prop("disabled", false);
    $('#PendingIsReserve').prop("disabled", false);

    $.ajax({
        type: "POST",
        url: GetPath(CommonVariable.modelName() + "/Edit"),
        data: $(this).closest('form').serialize(),
        dataType: "html",
        success: function (result) {
            if (!isNull(result)) {
                $('.itemEditContainer').html(result);
                $(".itemEditContainer").find("input[type='radio']:checked").each(function () {
                    $(this).closest("label.btn").addClass("active");
                });
                $('#CheckBoxStatus').bootstrapToggle();
                $('#shippingInfo').bootstrapToggle();
                $('#showReceipt').bootstrapToggle();

                //ShowAlertsMessage();
                ShowAlertsMessage('.itemEditMessageContainer', true);
                if (!isNull(result.Errors))
                {
                    $('.itemEditValidationMessageContainer').html(result.Errors);
                }
                if (!isNull(result.Success) )
                {
                    $('.itemEditMessageContainer').html(result.Success);
                }

                //For New, both can be unchecked
                if ($('.TXPaymentRadio #NewPaymentType').is(':checked')) {
                    $("#NewTXPaymentContainer").addClass("active");
                    $("#NewWeekPaymentContainer").removeClass("active");
                }
                if ($('.WeekPaymentRadio #WeekNewPaymentType').is(':checked')) {
                    $("#NewTXPaymentContainer").removeClass("active");
                    $("#NewWeekPaymentContainer").addClass("active");
                }

                if ($('#HasFundingConfigurationChanges').val() === 'true') {
                    $('#NewPaymentButtonSection').hide();
                    $('#CancelPaymentButtonSection').show();
                    $('#NewPaymentConfigurationInputSection').show();
                }
                else {
                    $('#NewPaymentButtonSection').show();
                    $('#CancelPaymentButtonSection').hide();
                    $('#NewPaymentConfigurationInputSection').hide();
                }

                if ($('#HasReserveConfigurationChanges').val() === 'true') {
                    $('#NewReserveButtonSection').hide();
                    $('#CancelReserveButtonSection').show();
                    $('#NewReserveConfigurationInputSection').show();
                }
                else {
                    $('#NewReserveButtonSection').show();
                    $('#CancelReserveButtonSection').hide();
                    $('#NewReserveConfigurationInputSection').hide();
                }
                
                SetReserveStartDateInputVisibility();
                SetReserveOtherFieldsVisibility();

                $('#CheckBoxStatus').bootstrapToggle();
                $('#responseFlag').bootstrapToggle();
                $('#shippingInfo').bootstrapToggle();
                $('#showReceipt').bootstrapToggle();
                $('#LinkPayment').bootstrapToggle();
                $('#OnlyAllowPriorityBin').bootstrapToggle();

                //MID
                $('#IsRefund').bootstrapToggle();
                $('#IsChargeback').bootstrapToggle();
                $('#IsEmailToMerchant').bootstrapToggle();
                $('#IsReserve').bootstrapToggle();
                $('#PendingIsReserve').bootstrapToggle();
                $('#NewIsReserve').bootstrapToggle();
            };
        },
        error: function (textStatus, errorThrown) {
            Success = false;
        }

    })

    //MIDFunding - add back disabled attribute
    $('#PaymentType').attr('disabled', true);
    $('#IsReserve').prop("disabled", true);
    $('#PendingIsReserve').prop("disabled", true);
    return false;
});

$(document).on("click", ".itemBack", function (e) {
    e.preventDefault();
    $('.itemDetailsContainer').hide();
    $('.itemCreateContainer').hide();
    $('.itemEditContainer').hide();
    $('.itemDetailsContainer').empty();
    $('.itemCreateContainer').empty();
    $('.itemEditContainer').empty();
    $('.itemsListingContainer').show();
    return false;
});

$(document).on("click", ".itemBackRefresh", function (e) {
    e.preventDefault();
    $('.itemDetailsContainer').hide();
    $('.itemCreateContainer').hide();
    $('.itemEditContainer').hide();
    //alert();
    //$('.itemDetailsContainer').empty();
    //$('.itemCreateContainer').empty();
    //$('.itemEditContainer').empty();
    $('#tbllist').bootstrapTable('refresh');
    $('.itemsListingContainer').show();
    return false;
});

//for Detail Mechant Profile
$(document).on("click", ".itemBackMechantRefresh", function (e) {
    e.preventDefault();
    $('.itemDetailsContainer').hide();
    $('.itemCreateContainer').hide();
    $('.itemEditContainer').hide();
    if ($('#UsersType').val() === 'W') {
        $('#tbllist').bootstrapTable('refresh');
    }
    $('.itemsListingContainer').show();
    return false;
}); 
//$(document).on("change", "#CountryCode", function () {
//       if ($(this).val() !== "") {
//           //alert($(this).val());
//           $("#StateCode").PopulateStates($(this).val());
//       }
//   }); 

$(document).on("change", "#CompCode", function () {
    if ($(this).val() !== "")
        $("#DistCode").PopulateDistributor($(this).val());
});

$(document).on("change", "#CountryCode", function () {
    if ($(this).val() !== "") {
        //alert($(this).val());
        $("#StateCode").PopulateStates($(this).val());
    }
});

   
function listSearch() {
   
    $('#tbllist').bootstrapTable('refreshOptions', { pageNumber: 1 });
    
}
function itemDetails(jsonData) {
    $.ajax({
        type: "get",
        url: GetPath(modelName + "/Details"),
        data: (jsonData),
        contentType: "application/json",
        dataType: "html",
        success: function (result) {

            if (!isNull(result)) {
                $('.itemDetailsContainer').html(result);
                $('.itemDetailsContainer').show();
                $('.itemCreateContainer').empty();
                $('.itemEditContainer').empty();
                $('.itemsListingContainer').hide();

                $('#CheckBoxStatus').bootstrapToggle();             
                $('#responseFlag').bootstrapToggle();
                $('#shippingInfo').bootstrapToggle();
                $('#showReceipt').bootstrapToggle();
                $('#LinkPayment').bootstrapToggle();


            }
        }
    });
}

function itemNewWindowDetails(jsonData) {
    window.open(GetPath(modelName + "/Details?" + $.param(jsonData, false)), '_blank', 'width=300,height=250,scrollbars=1');
}

function itemTransactionRecordDetails(jsonData) {
    window.open(GetPath("DailyTransactionReport/Details?" + $.param(jsonData, false)), '_blank', 'width=300,height=250,scrollbars=1');
}

function itemTransactionRecordList(jsonData) {
    window.open(GetPath("DailyTransactionReport/ListTransactionForSettlementException?" + $.param(jsonData, false)), '_blank', 'width=300,height=250,scrollbars=1');
}


function itemCreate(e) {

    $.ajax({
        type: "get",
        url: GetPath(CommonVariable.modelName() + "/Create"),
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            if (!isNull(result)) {

                $('.itemCreateContainer').html(result);
                $('.itemCreateContainer').show();
                $('.itemEditContainer').empty();
                $('.itemDetailsContainer').empty();
                $('.itemsListingContainer').hide();
                ShowAlertsMessage('.itemCreateMessageContainer', true);

                $('.routeDateControl').datetimepicker({
                    viewMode: 'days',
                    format: CommonVariable.standardDateFormat(),
                    ignoreReadonly: true,
                    keepOpen: false
                });
                var dateNow = new Date();
                $('.routeTimeControl').datetimepicker({
                    format: 'LT',
                    defaultDate: moment(dateNow).hours(0).minutes(0).seconds(0).milliseconds(0),
                    ignoreReadonly: true
                });
                $('.routeDateDayControl').datetimepicker({
                    viewMode: 'days',
                    format: CommonVariable.standardDateFormat(),
                    ignoreReadonly: true,
                    keepOpen: false
                });

                var start1_date = $(".routeDateDayControl").data('DateTimePicker');
                var datePickerEnd = moment().startOf('day');//.subtract(1, 'days')
                if (typeof start1_date !== 'undefined' && start1_date)
                {
                    start1_date.maxDate(datePickerEnd);
                }


                $('.routeDateYearControl').datetimepicker({
                    viewMode: 'years',
                    format: CommonVariable.standardDateFormat(),
                    ignoreReadonly: true
                });

                var start_date = $(".routeDateYearControl").data('DateTimePicker');
                datePickerEnd = moment().startOf('day').subtract(1, 'days');
                if (typeof start_date !== 'undefined' && start_date) {
                    start_date.maxDate(datePickerEnd);
                }


                var form = $('.itemNew').closest("form");
                $(form).removeData("validator");
                $(form).removeData("unobtrusiveValidation");
                $.validator.unobtrusive.parse(form);

                $('#OnlyAllowPriorityBin').bootstrapToggle();

            }
        }
    });
}

function itemUploadCreate(e) {
    $.ajax({
        type: "get",
        url: GetPath(CommonVariable.modelName() + "/Create"),
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            if (!isNull(result)) {

                $('.itemCreateContainer').html(result);
                $('.itemCreateContainer').show();
                $('.itemEditContainer').empty();
                $('.itemDetailsContainer').empty();
                $('.itemsListingContainer').hide();
                $('.itemsExceptionContainer').hide();
                $('.itemsReconContainer').hide();

                //Allows only yesterday as max, does not allow empty
                $('.routeDateDayControl2').datetimepicker({
                    viewMode: 'days',
                    format: CommonVariable.standardDateFormat(),
                    ignoreReadonly: true,
                    keepOpen: false,
                    useCurrent: false, //Prevents setting max date in datepicker from selecting a date
                    showClear: false
                });
                var datePickerEnd = moment().startOf('day');//.subtract(1, 'days')

                var start2_date = $(".routeDateDayControl2").data('DateTimePicker');
                if (typeof start2_date !== 'undefined' && start2_date) {
                    start2_date.maxDate(datePickerEnd);
                    start2_date.disabledDates([datePickerEnd]); //Workaround to unable to pick max date issue
                }
            }
        }
    });
}

function itemEdit(jsonData) {

    $.ajax({
        type: "get",
        url: GetPath(CommonVariable.modelName() + "/Edit"),
        data: (jsonData),
        contentType: "application/json",
        dataType: "html",
        success: function (result) {

            if (!isNull(result)) {
                $('.itemEditContainer').html(result);
                $('.itemEditContainer').show();

                if ($('#CurrentStatus').val() === '1')
                {
                    $(".itemEditContainer :input").attr("disabled", true);
                    $(".itemEditFooter :input").attr("disabled", false);

                }


                $(".itemEditContainer").find("input[type='radio']:checked").each(function () {
                    $(this).closest("label.btn").addClass("active");
                });

                $('.itemCreateContainer').empty();
                $('.itemDetailsContainer').empty();
                $('.itemsListingContainer').hide();

                $('.routeDateControl').datetimepicker({
                    viewMode: 'days',
                    //sideBySide: true,
                    //inline: true,
                    format: CommonVariable.standardDateFormat(),
                    ignoreReadonly: true,
                    keepOpen: false
                    //minDate: datePickerStart //moment().startOf('day')
                });
                        var dateNow = new Date();
                      
                $('.routeDateDayControl').datetimepicker({
                    viewMode: 'days',
                    format: CommonVariable.standardDateFormat(),
                    ignoreReadonly: true,
                    keepOpen: false
                });

                var start1_date = $(".routeDateDayControl").data('DateTimePicker');
                var datePickerEnd = moment().startOf('day');//.subtract(1, 'days')
                if (typeof start1_date !== 'undefined' && start1_date) {
                    start1_date.maxDate(datePickerEnd);
                }

                var dateNow = new Date();
                $('.routeTimeControl').datetimepicker({
                    format: 'LT',
                    defaultDate: moment(dateNow).hours(0).minutes(0).seconds(0).milliseconds(0),
                    ignoreReadonly: true
                });

                var form = $('.itemEditContainer').closest("form");
                $(form).removeData("validator");
                $(form).removeData("unobtrusiveValidation");
                $.validator.unobtrusive.parse(form);

                //for Merchant Profile
                if ($('#reseller').val() === '') {
                    $('.contactReseller').hide();
                }
                else {
                    $('.contactReseller').show();
                }
                //

                ////For Mid
                //if ($('.TXPaymentRadio #PaymentType').is(':checked')) {
                //    $("#TXPaymentContainer").addClass("active");
                //    $("#WeekPaymentContainer").removeClass("active");
                //}
                //else
                //{
                //    $("#TXPaymentContainer").removeClass("active");
                //    $("#WeekPaymentContainer").addClass("active");
                //}
                
                if ($('.TXPaymentRadio #NewPaymentType').is(':checked')) {
                    $("#NewTXPaymentContainer").addClass("active");
                    $("#NewWeekPaymentContainer").removeClass("active");
                }

                if ($('.WeekPaymentRadio #WeekNewPaymentType').is(':checked'))
                {
                    $("#NewTXPaymentContainer").removeClass("active");
                    $("#NewWeekPaymentContainer").addClass("active");
                }

                //if ($('.TXPaymentRadio #IsCreatePendingReserve').is(':checked')) {
                //    $("#PendingReserveTXPaymentContainer").addClass("active");
                //}

                if ($('#HasFundingConfigurationChanges').val() === 'true') {
                    $('#NewPaymentButtonSection').hide();
                    $('#CancelPaymentButtonSection').show();
                    $('#NewPaymentConfigurationInputSection').show();
                }
                else {
                    $('#NewPaymentButtonSection').show();
                    $('#CancelPaymentButtonSection').hide();
                    $('#NewPaymentConfigurationInputSection').hide();
                }

                if ($('#HasReserveConfigurationChanges').val() === 'true') {
                    $('#NewReserveButtonSection').hide();
                    $('#CancelReserveButtonSection').show();
                    $('#NewReserveConfigurationInputSection').show();
                }
                else {
                    $('#NewReserveButtonSection').show();
                    $('#CancelReserveButtonSection').hide();
                    $('#NewReserveConfigurationInputSection').hide();
                }

                SetReserveStartDateInputVisibility();
                SetReserveOtherFieldsVisibility();

                $(document).on("change", "#NewIsReserve", function (e) {
                    SetReserveStartDateInputVisibility();
                    SetReserveOtherFieldsVisibility();
                });
                
                $('#CheckBoxStatus').bootstrapToggle();
                $('#responseFlag').bootstrapToggle();
                $('#shippingInfo').bootstrapToggle();
                $('#showReceipt').bootstrapToggle();
                $('#LinkPayment').bootstrapToggle();
                $('#OnlyAllowPriorityBin').bootstrapToggle();

                //MID
                $('#IsRefund').bootstrapToggle();
                $('#IsChargeback').bootstrapToggle();
                $('#IsEmailToMerchant').bootstrapToggle();
                $('#IsReserve').bootstrapToggle();
                $('#PendingIsReserve').bootstrapToggle();
                $('#NewIsReserve').bootstrapToggle();
            }
        }
    });
}


//Support AntiForgeryToken
function addRequestVerificationToken(data) {
    data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    return data;
};

function validationPartialView(classname) {
    var form = $(classname).closest("form");
    $(form).removeData("validator");
    $(form).removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
};

function parseFloat2Decimals(value) {
    return parseFloat(parseFloat(value));
}
function parseFloat2Decimals(value, decimalPlaces) {
    return parseFloat(parseFloat(value).toFixed(decimalPlaces));
}

function precisionRound(number, precision) {
    var factor = Math.pow(10, precision);
    return Math.round(number * factor) / factor;
}

function twoDecimalPointFormatter(value, row, index) {
    if (value !== null) {
        return parseFloat(precisionRound(value, 2)).toFixed(2);
    }
    return value;
}

//Returns positive or negative formats depending on value
function customStringFormatter(value, row, index) {
    if (value !== null)
    {
        if (value < 0)
        {
            return "(" + parseFloat(precisionRound(Math.abs(value), 2)).toFixed(2) + ")";
        }
        else
        {
            return parseFloat(precisionRound(value, 2)).toFixed(2);
        }
    }
    return value;
}

//Returns negative format regardless of value
function customStringShowNegativeFormatter(value, row, index) {
    if (value !== null)
    {
        return "(" + parseFloat(precisionRound(Math.abs(value), 2)).toFixed(2) + ")";
    }
    return value;
}

//For Login Images
$(document).on("click", ".loginimageDiv img", function (e) {
    e.stopPropagation();

    //alert(123456);
    $(".loginimageDiv img").removeClass("selected");
    $(this).addClass("selected");

});

$(document).on("click", ".LoginImageRequestTac", function (e) {
    e.stopPropagation();
    $.ajax({
        type: "get",
        url: GetPath("User/GetLoginImageTac"),
        //data: (jsonData),
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            //alert(result);
            if (!isNull(result)) {
                $('.RequestTacMessageContainer').empty();
                //$('.RequestTacMessageContainer').val(result.Success);
                $('.RequestTacMessageContainer').ShowBoostrapAlert(result.Success, "success", true, true, false);
                //

            }
        }
    });


});

