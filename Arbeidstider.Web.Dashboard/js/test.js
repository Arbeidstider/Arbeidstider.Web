        $("#saveContact").click(function () {
            var data = {
                epost: $("#epost").val(),
                duplicateCheckFields: "epost"
            }

            $.ajax({
                url: "http://www.nettsvar.no/mhrestapi/Contact?apikey=A02FC2813FEB43CB98DED102EA960CB5&account=BRINGDIALOG_1001",
                type: "post",
                data: data,
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        if (data == "OK") {
                            $('#resultMessageOk').text(" Takk, du er registrert");
                            document.getElementById("OkAlert").style.display = "block";
                            document.getElementById("ErrorAlert").style.display = "none";
                        } else if (data == "ERROR") {
                            $('#resultMessageError').text("Noe gikk desverre galt. Vennligst prøv igjen senere.");
                            document.getElementById("ErrorAlert").style.display = "block";
                            document.getElementById("OkAlert").style.display = "none";

                        } else if (data == "INVALID") {
                            $('#resultMessageError').text("Vennligst skriv inn en gyldig epostadresse.");
                            document.getElementById("ErrorAlert").style.display = "block";
                            document.getElementById("OkAlert").style.display = "none";
                        } else if (data == "DUPLICATE") {
                            $('#resultMessageError').text("Litt vell overivrig? Du er vist allerede registrert..");
                            document.getElementById("ErrorAlert").style.display = "block";
                            document.getElementById("OkAlert").style.display = "none";
                        }
                    } else {
                        $('#resultMessageError').text("Noe gikk desverre galt. Vennligst prøv igjen senere.");
                        document.getElementById("ErrorAlert").style.display = "block";
                        document.getElementById("OkAlert").style.display = "none";
                    }
                },
                error: function () {
                    $('#resultMessageError').text("Noe gikk desverre galt. Vennligst prøv igjen senere. Feilkode: #ERR001");
                    document.getElementById("ErrorAlert").style.display = "block";
                    document.getElementById("OkAlert").style.display = "none";
                }
            });
        });