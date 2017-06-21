<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<DataContracts.FolksModel>>"  AutoEventWireup="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title>Index</title>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>    
    <script src="http://ajax.microsoft.com/ajax/jQuery.Validate/1.6/jQuery.Validate.min.js" type="text/javascript"></script>

    <!--i had the choice to put all of the styles into a separate file and then reference it, this might be a better practice,but
    for the purposes of this code sample i thought it may be easier for the reader to see it all in pretty much one place-->
     <!--css scripting-->
    <style type="text/css">
        
        p
        {
            text-align:center;
            font:12px arial,sans-serif;
            
        }
        
        h1 
        { 
        background-color:#FFFFFF;
        font-family:Verdana; 
        font-size:25px; 
        font-weight:bold; 
        color:#8b8dbb; 
        text-align:center;
        padding-bottom:20px;
        }
             
        .Grid { border: solid 1px #FFFFFF; }
        .Grid td
        {
        border: solid 1px #FFFFFF;
        margin: 1px 1px 1px 1px;
        padding: 1px 1px 1px 1px;
        text-align: left;
        font:12px arial,sans-serif;

        }
        .GridHeader
        {
        font-weight: bold;
        background-color: #8b8dbb;
        font:15px arial,sans-serif;

        }
        .GridItem
        {
        background-color: #e6e6e6;
        font:12px arial,sans-serif;

        }

        .GridAltItem
        {
        background-color: white;
        font:12px arial,sans-serif;
        }
        
        .select
        {
        	background-color:#c0c0c0;
        	font:12px arial,sans-serif;}
        
    </style>
</head>
<body>
    <div>
    <form id="Form1" runat="server">

        <h1>Display Folks</h1>
        
        <p>
        <asp:GridView ID="FolksGridView" runat="server" AutoGenerateColumns="false"
        OnDataBound="FolksGridView_DataBound" CssClass="Grid" GridLines="Horizontal" HorizontalAlign="Center" 
         ViewStateMode="Disabled">
            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
            <AlternatingRowStyle CssClass="GridAtlItem"/>
            <RowStyle CssClass="GridItem" />
            <SelectedRowStyle CssClass="select" />
            
            <Columns>                
                <asp:BoundField  HeaderText="First Name" DataField="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                <asp:BoundField DataField="BirthLocation" HeaderText = "Birth Location" SortExpression="BirthLocation" />
                 
            </Columns>
        </asp:GridView>  
        </p>   
    </form>
    </div>
</body>

    <!--JQuery code may also have been best placed in a separate file in real practice, but for demo might be easier 
    to follow/read by placing it directly in the page-->
     <script language="javascript" type="text/javascript">
         $(document).ready(function () {

             //add the alternating styles
             $("#<%=FolksGridView.ClientID %> tr:odd td").addClass("GridAltItem");
             $("#<%=FolksGridView.ClientID %> tr:even td").addClass("GridItem");

             //find non table header rows
             $("#<%=FolksGridView.ClientID %> tr").filter(":not(:has(table, th))").mousedown(function () {
                 $("#<%=FolksGridView.ClientID %> tr td").removeClass("select");

                 //reset any that did have select style - not very efficient room for improvement here for sure
                 $("#<%=FolksGridView.ClientID %> tr:odd td").addClass("GridAltItem");
                 $("#<%=FolksGridView.ClientID %> tr:even td").addClass("GridItem");

                 //longest row due to td spanning rows
                 var tds = $("#<%=FolksGridView.ClientID %> tr:nth-child(2)").find("td");

                 //highlight cells in the row
                 if ($(this).find("td").length < tds.length) {
                     $(this).find("td").addClass("select");
                 }
                 else {
                     $(this).find("td").not(":last").addClass("select");
                 }                


             });

         });
     </script>

    <!--server controls scripting-->
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            // get the value from the postback parameters
            if (Request.Params["__EVENTTARGET"] == FolksGridView.UniqueID)
            {
                string arg = Request.Params["__EVENTARGUMENT"];
                if (!string.IsNullOrEmpty(arg))
                {
                    string[] idThenRow = arg.Split('|');
                    if (idThenRow.Length == 2)
                    {
                        string id = idThenRow[0];
                        string row = idThenRow[1];
                        LoadAndBind(id, row);
                    }
                }

            }
            else
            {
                LoadAndBind();
            }
            
        }
        protected void LoadAndBind()
        {
            var model = ViewData.Model;
            FolksGridView.DataSource = model;
            FolksGridView.SelectedIndex = 0;
            FolksGridView.DataBind();
        }

        protected void LoadAndBind(string id, string row)
        {
            var modelForBio = ViewData.Model.FindAll(s => s.Id.ToString() == id);

            var model = ViewData.Model;
            FolksGridView.DataSource = model;            
            FolksGridView.DataBind();
            FolksGridView.Rows[0].Cells[FolksGridView.Rows[0].Cells.Count - 1].Text = modelForBio[0].Bio;            
               
        }     




        protected void FolksGridView_DataBound(object sender, EventArgs e)        
        {             
            //here we are waiting until the data is fully bound to the GridView so that we can 
            //add a spanning over the number of rows column that will hold the BIO when a row is selected

           TableCell objtablecell = new TableCell();
           objtablecell.ID = "BiographyTarget";
            //objtablecell.ColumnSpan = 1;
           objtablecell.Width = Unit.Percentage(75);
            objtablecell.RowSpan = FolksGridView.Rows.Count;
            objtablecell.Wrap = true;
            FolksGridView.Rows[0].Cells.Add(objtablecell);         

            //Add the header text for our new column
           TableHeaderCell headerCell = new TableHeaderCell();          
           headerCell.Text = "Biography";
           FolksGridView.HeaderRow.Cells.AddAt(FolksGridView.Columns.Count  ,headerCell);
            
            //now lets give each row an ID so that we can do some Jquery to select the row later if we wish
            int rowCounter = 1;
           foreach (GridViewRow row in FolksGridView.Rows)
           {
               row.ID = "row_" + rowCounter.ToString();              
               string id = this.Model[rowCounter - 1].Id.ToString();
               row.Attributes["onmousedown"] = ClientScript.GetPostBackClientHyperlink(this.FolksGridView, id + "|" + row.RowIndex.ToString());
               rowCounter++;
               
           }               

 }    

    </script>
</html>
