
var CommonVariable = CommonVariable || (function () {
    var _CommonModelName;

    return {
        init: function (modelName) {
            _CommonModelName = modelName;
        },
        modelName: function () {
            return _CommonModelName;
        },
        standardDateFormat: function () {
            return "dd-MM-yyyy";
        },
        standardDateTimeFormat: function () {
            return "dd-MM-yyyy HH:mm";
        },
        standardDetailDateFormat: function () {
            return "dd MMM yyyy";
        },
        standardDetailDateTimeFormat: function () {
            return "dd MMM yyyy HH:mm";
        }
    };
}());

var CommonIconsTypes = CommonIconsTypes || (function () {
    
    return {

        Search: function () {
            return "fa-solid fa-magnifying-glass fa-lg";
        },
        New: function () {
            return "fa-solid fa-circle-plus fa-lg";
        },
        Edit: function () {
            return "fa-solid fa-pen-to-square fa-lg";
        },
        Save: function () {
            return "fa-solid fa-floppy-disk fa-lg";
        },
        Reset: function () {
            return "fa-solid fa-rotate fa-lg";
        },
        Back: function () {
            return "fa-solid fa-reply-all fa-lg";
        },
        Required: function () {
            return "far fa-asterisk fa-sm faRequired";
        },
        Calendar: function () {
            return "fa fa-calendar";
        }
    };
}());

(function ($) {

    //$.fn.EmptyDropDownList = function (controlName) {
    //    $("#" + controlName).empty();
    //    $("#" + controlName).append("<option value=''>-</option>");
    //}

   

    $.fn.center = function () {
        var heightRatio = ($(window).height() !== 0)
            ? this.outerHeight() / $(window).height() : 1;
        var widthRatio = ($(window).width() !== 0)
            ? this.outerWidth() / $(window).width() : 1;

        this.css({
            position: 'fixed',
            margin: 0,
            top: (50 * (1 - heightRatio)) + "%",
            left: (50 * (1 - widthRatio)) + "%"
        });

        return this;
    };

    $.fn.EmptyDropDownList = function () {
        $(this).empty();
        $(this).append("<option value=''>-</option>");
    };

    $.fn.EmptyDropDownListWithDefaultText = function () {
        var emptyOptionText = $(this).find('option[value=""]').text();
        $(this).empty();
        if (emptyOptionText !== null && emptyOptionText !== "") {
            $(this).append("<option value=''>" + emptyOptionText + "</option>");
        }
    };

    $.fn.PopulateDistributor = function (CompanyCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetDistributorList"),
            data: ({ CompanyCode: CompanyCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateRecipientGroup = function (CompanyCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetRecipientGroupList"),
            data: ({ CompanyCode: CompanyCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateStates = function (Country) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetStateByCountry"),
            data: ({ CountryCode: Country }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateBranch = function (DistCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetBranchList"),
            data: ({ DistributorCode: DistCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateLocation = function (BranchCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetLocationList"),
            data: ({ BranchCode: BranchCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };
  
    $.fn.PopulateProcessorTag = function (ProcessorCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetProcessorTagListByProcessorCode"),
            data: ({ ProcessorCode: ProcessorCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateProcessorMCC = function (ProcessorCode, IndustryCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetProcessorMCCListByIndustryCode"),
            data: ({ ProcessorCode: ProcessorCode, IndustryCode: IndustryCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateMCCByIndustry = function (IndustryCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetMCCListByIndustryCode"),
            data: ({ IndustryCode: IndustryCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateProcessorIndustryList = function (ProcessorCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetProcessorIndustryListByProcessorCode"),
            data: ({ ProcessorCode: ProcessorCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateProcessorCurrency = function (ProcessorCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetProcessorCurrencyListByProcessorCode"),
            data: ({ ProcessorCode: ProcessorCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateMidCustomerCode = function (CustomerCode) {
        $(this).EmptyDropDownListWithDefaultText();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetMidListByCustomerCode"),
            data: ({ CustomerCode: CustomerCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateTidCustomerCode = function (CustomerCode) {
        $(this).EmptyDropDownListWithDefaultText();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetTidListByCustomerCode"),
            data: ({ CustomerCode: CustomerCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateCurrencyByCustomerCode = function (CustomerCode) {
        $(this).EmptyDropDownListWithDefaultText();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetCurrencyByCustomerCode"),
            data: ({ CustomerCode: CustomerCode }),
            dataType: "json",
            success: function (result) {
                if (result !== null) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    }

    $.fn.PopulateAccountManager = function (PartnerCode) {
        $(this).EmptyDropDownList();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetAccountManagerListByPartnerCode"),
            data: ({ PartnerCode: PartnerCode }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.PopulateCurrencyCodeByMerchantCodes = function (merchantCodes) {
        $(this).EmptyDropDownListWithDefaultText();
        var control$ = $(this);
        $.ajax({
            type: "GET",
            url: GetPath(CommonVariable.modelName() + "/GetCurrencyCodeListByMerchantCodes"),
            data: ({ merchantCodes: merchantCodes }),
            dataType: "json",
            success: function (result) {
                if (!isNull(result)) {
                    $.each(result, function (index, data) {
                        control$.append("<option value='" + data.Value + "'>" + data.Text + "</option>");
                    });
                }
            }
        });
    };

    $.fn.ShowBoostrapAlert = function (message, style, dismissable, empty, autoclose) {
        $(this).EmptyDropDownList();
        var control$ = $(this);

        var html = "";
        var dismissableClass = dismissable ? "alert-dismissable" : null;
        var autoDisappearClass = autoclose ? "alert-disappear" : null;
        html = "<div class='alertdiv alert alert-" + style + " " + dismissableClass + " " + autoDisappearClass + "' >";



        if (dismissable) {
            html = html + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>";


        }
        html = html + message + "</div>";
        control$.html(html);      
        $(".alert-disappear").delay(8000).fadeOut(300);
    };

   
})(jQuery);