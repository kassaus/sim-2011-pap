<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true"
	CodeBehind="Home.aspx.cs" Inherits="Lives.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript" src="Silverlight.js"></script>
	<script type="text/javascript">
		function onSilverlightError(sender, errorArgs) {
			// The error message to display.
			var errorMsg = "Silverlight Error: \n\n";

			// Error information common to all errors.
			errorMsg += "Error Type:    " + errorArgs.errorType + "\n";
			errorMsg += "Error Message: " + errorArgs.errorMessage + "\n";
			errorMsg += "Error Code:    " + errorArgs.errorCode + "\n";

			// Determine the type of error and add specific error information.
			switch (errorArgs.errorType) {
				case "RuntimeError":
					// Display properties specific to RuntimeErrorEventArgs.
					if (errorArgs.lineNumber != 0) {
						errorMsg += "Line: " + errorArgs.lineNumber + "\n";
						errorMsg += "Position: " + errorArgs.charPosition + "\n";
					}
					errorMsg += "MethodName: " + errorArgs.methodName + "\n";
					break;
				case "ParserError":
					// Display properties specific to ParserErrorEventArgs.
					errorMsg += "Xaml File:      " + errorArgs.xamlFile + "\n";
					errorMsg += "Xml Element:    " + errorArgs.xmlElement + "\n";
					errorMsg += "Xml Attribute:  " + errorArgs.xmlAttribute + "\n";
					errorMsg += "Line:           " + errorArgs.lineNumber + "\n";
					errorMsg += "Position:       " + errorArgs.charPosition + "\n";
					break;
				default:
					break;
			}
			// Display the error message.
			alert(errorMsg);
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="corpo_user" runat="server">
	<div class='left'>
		<asp:Literal ID="Video" runat="server"></asp:Literal>
	</div>
	
</asp:Content>