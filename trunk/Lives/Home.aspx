<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true"
	CodeBehind="Home.aspx.cs" Inherits="Lives.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript" src="Silverlight.js"></script>
	<script type="text/javascript">
		function onSilverlightError(sender, args) {
			var appSource = "";
			if (sender != null && sender != 0) {
				appSource = sender.getHost().Source;
			}

			var errorType = args.ErrorType;
			var iErrorCode = args.ErrorCode;

			if (errorType == "ImageError" || errorType == "MediaError") {
				return;
			}

			var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

			errMsg += "Code: " + iErrorCode + "    \n";
			errMsg += "Category: " + errorType + "       \n";
			errMsg += "Message: " + args.ErrorMessage + "     \n";

			if (errorType == "ParserError") {
				errMsg += "File: " + args.xamlFile + "     \n";
				errMsg += "Line: " + args.lineNumber + "     \n";
				errMsg += "Position: " + args.charPosition + "     \n";
			}
			else if (errorType == "RuntimeError") {
				if (args.lineNumber != 0) {
					errMsg += "Line: " + args.lineNumber + "     \n";
					errMsg += "Position: " + args.charPosition + "     \n";
				}
				errMsg += "MethodName: " + args.methodName + "     \n";
			}

			throw new Error(errMsg);
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="corpo_user" runat="server">
	<div class='left'>
		<asp:Literal ID="Video" runat="server"></asp:Literal>
	</div>
	
</asp:Content>