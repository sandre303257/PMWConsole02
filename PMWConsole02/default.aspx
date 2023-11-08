<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="default.aspx.vb" Inherits="PMWConsole02._Default" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
       <dx:ASPxGridView ID="MasterGrid" runat="server" DataSourceID="sdsMasterGrid" Theme="Aqua" EnableTheming="True" AutoGenerateColumns="False" KeyFieldName="ID" PreviewFieldName="TaskDescription" PreviewEncodeHtml="false">
        <SettingsDetail ShowDetailRow="True"></SettingsDetail>
        <Settings ShowPreview="true" />

        <Templates>
            <DetailRow>
                <br /><b>Job Description</b><br />
                <dx:ASPxGridView ID="DetailGridJob" runat="server" AutoGenerateColumns="False" DataSourceID="sdsDetailGrid" EnableTheming="True" KeyFieldName="ID" OnBeforePerformDataSelect="DetailGridJob_BeforePerformDataSelect" Theme="Aqua">
                    <Settings ShowColumnHeaders="False" ShowGroupButtons="False" />
                    <SettingsBehavior AllowGroup="False" />
                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False"></SettingsDataSecurity>

                    <SettingsPopup>
                        <FilterControl AutoUpdatePosition="False">
                        </FilterControl>
                    </SettingsPopup>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" Visible="False" VisibleIndex="1">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Job" VisibleIndex="2" ReadOnly="True">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
               <%-- <br /><b>Task Description</b><br />
                
                <dx:ASPxGridView ID="DetailGridTaskDescription" runat="server" AutoGenerateColumns="False" DataSourceID="sdsDetailGrid" EnableTheming="True" KeyFieldName="ID" Theme="Aqua" OnBeforePerformDataSelect="DetailGridTaskDescription_BeforePerformDataSelect">
                    <Settings ShowColumnHeaders="False" ShowGroupButtons="False" />
                    <SettingsBehavior AllowGroup="False" />
                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False"></SettingsDataSecurity>

                    <SettingsPopup>
                        <FilterControl AutoUpdatePosition="False">
                        </FilterControl>
                    </SettingsPopup>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" Visible="False" VisibleIndex="0">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
                            <PropertiesTextEdit EncodeHtml="False">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>--%>
                
                <br /><b>Task Notes</b><br />
                <dx:ASPxGridView ID="DetailGridTaskNotes" runat="server" DataSourceID="sdsDetailGrid" Theme="Default" AutoGenerateColumns="False" KeyFieldName="ID" OnBeforePerformDataSelect="DetailGridTaskNotes_BeforePerformDataSelect" EnableTheming="True">
                    <SettingsEditing Mode="Batch"></SettingsEditing>

                    <Settings ShowColumnHeaders="False" ShowGroupButtons="False" />
                    <SettingsBehavior AllowGroup="False" />
                    <SettingsDataSecurity AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>

                    <SettingsPopup>
                        <FilterControl AutoUpdatePosition="False">
                        </FilterControl>
                    </SettingsPopup>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" Visible="False" VisibleIndex="0">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataMemoColumn FieldName="Notes" VisibleIndex="1" CellRowSpan="5">
                            <PropertiesMemoEdit Columns="1"></PropertiesMemoEdit>

                            <CellStyle BackColor="White">
                                <Border BorderColor="White" BorderWidth="0px"></Border>
                            </CellStyle>
                        </dx:GridViewDataMemoColumn>

                        <dx:GridViewCommandColumn ShowEditButton="True" ShowUpdateButton="True" ShowCancelButton="True" ButtonRenderMode="Button" ButtonType="Button" VisibleIndex="2" Visible="False"></dx:GridViewCommandColumn>
                    </Columns>
                </dx:ASPxGridView>
            
            </DetailRow>
        </Templates>

        <SettingsPager PageSize="100"></SettingsPager>

        <SettingsEditing Mode="Batch">
            <BatchEditSettings KeepChangesOnCallbacks="True"></BatchEditSettings>
        </SettingsEditing>

        <Settings ShowHeaderFilterButton="True" ShowGroupButtons="False" />
        <SettingsBehavior AllowGroup="False" AllowFocusedRow="True" AllowSelectByRowClick="True"></SettingsBehavior>

        <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />

        <SettingsPopup>
            <FilterControl AutoUpdatePosition="False"></FilterControl>
        </SettingsPopup>

        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Button" ButtonType="Button" Visible="False">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="1" Visible="False">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Job" VisibleIndex="2" ReadOnly="True" Caption="Job Number">

                <SettingsHeaderFilter Mode="CheckedList"></SettingsHeaderFilter>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                <CellStyle Wrap="False"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProductType" VisibleIndex="10" ReadOnly="True">
                <SettingsHeaderFilter Mode="CheckedList">
                </SettingsHeaderFilter>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Department" VisibleIndex="4" ReadOnly="True">
                <SettingsHeaderFilter Mode="CheckedList"></SettingsHeaderFilter>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="DateCompleted" VisibleIndex="7" Caption="Finish Date">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="Component" VisibleIndex="3" ReadOnly="True">
                <SettingsHeaderFilter Mode="CheckedList"></SettingsHeaderFilter>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="DueDate" Caption="Due Date" VisibleIndex="5">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataCheckColumn FieldName="MarkedCompleted" VisibleIndex="6" Caption="Finished">
                <SettingsHeaderFilter Mode="CheckedList"></SettingsHeaderFilter>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataComboBoxColumn FieldName="TaskStatus" VisibleIndex="8" Caption="Task Status">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="In Progress" Value="In Progress"></dx:ListEditItem>
                        <dx:ListEditItem Text="Completed" Value="Completed"></dx:ListEditItem>
                        <dx:ListEditItem Text="On Hold" Value="On Hold"></dx:ListEditItem>
                    </Items>

                </PropertiesComboBox>
                <SettingsHeaderFilter Mode="CheckedList"></SettingsHeaderFilter>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="Description" Caption="Job Description" VisibleIndex="9" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                <CellStyle Wrap="True"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TaskDescription" Visible="False" VisibleIndex="11"></dx:GridViewDataTextColumn>
        </Columns>
        <Styles>

            <SelectedRow Font-Bold="False" BackColor="#0066FF" ForeColor="White"></SelectedRow>
            <FocusedRow BackColor="#0000CC" ForeColor="White"></FocusedRow>
            <BatchEditModifiedCell ForeColor="Black">
            </BatchEditModifiedCell>
        </Styles>
    </dx:ASPxGridView>

    <asp:SqlDataSource ID="sdsMasterGrid" runat="server" ConnectionString='<%$ ConnectionStrings:PMW_ConsoleConnectionString2 %>' ProviderName='<%$ ConnectionStrings:PMW_ConsoleConnectionString2.ProviderName %>'
        SelectCommand="SELECT tasks.ID, CONCAT(`motor quotations`.`Job Number`, '-', products.Suffix) AS Job, CONCAT(products.Description, ' for ', `customer contact information`.`Company Name`) AS Description, productTypeT.ProductType, departments.Department, tasks.MarkedCompleted, tasks.DateCompleted, tasks.Component, tasks.DueDate, tasks.TaskStatus, tasks.`Task Description` AS TaskDescription FROM tasks INNER JOIN products ON tasks.ProductID = products.ID INNER JOIN productTypeT ON products.ProductTypeID = productTypeT.ProductTypeID INNER JOIN employees ON tasks.AssignedToID = employees.ID INNER JOIN departments ON employees.Department = departments.ID INNER JOIN `motor quotations` ON products.QuoteID = `motor quotations`.ID INNER JOIN `customer contact information` ON `motor quotations`.`Company ID Number` = `customer contact information`.ID WHERE (tasks.TaskStatus <> 'Completed') ORDER BY tasks.DueDate, tasks.Priority"
        UpdateCommand="UPDATE tasks SET MarkedCompleted = @MarkedCompleted, DateCompleted = @DateCompleted, TaskStatus = @TaskStatus, DueDate = @DueDate WHERE (ID = @ID)">
        <UpdateParameters>
            <asp:Parameter Name="MarkedCompleted" Type="String" />
            <asp:Parameter Name="TaskStatus" Type="String" />
            <asp:Parameter Name="DateCompleted" Type="Datetime" />
            <asp:Parameter Name="DueDate" Type="DateTime"/>
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
        </asp:SqlDataSource>
    
   <asp:SqlDataSource ID="sdsDetailGrid" runat="server" ConnectionString="<%$ ConnectionStrings:PMW_ConsoleConnectionString2 %>" ProviderName="<%$ ConnectionStrings:PMW_ConsoleConnectionString2.ProviderName %>"
        SelectCommand="SELECT tasks.ID, CONCAT(`motor quotations`.`Job Number`, '-', products.Suffix, ', ', products.Description, ' for ', `customer contact information`.`Company Name`) AS Job, tasks.`Task Description` AS Description, tasks.TaskStatus, tasks.Notes FROM tasks INNER JOIN products ON tasks.ProductID = products.ID INNER JOIN productTypeT ON products.ProductTypeID = productTypeT.ProductTypeID INNER JOIN employees ON tasks.AssignedToID = employees.ID INNER JOIN departments ON employees.Department = departments.ID INNER JOIN `motor quotations` ON products.QuoteID = `motor quotations`.ID INNER JOIN `customer contact information` ON `motor quotations`.`Company ID Number` = `customer contact information`.ID WHERE (tasks.ID = @ID) ORDER BY tasks.`DueDate`, tasks.Priority"
        UpdateCommand="UPDATE tasks SET Notes = @Notes, TaskStatus= @TaskStatus WHERE (ID = @ID)">
       
        <SelectParameters>
            <asp:SessionParameter Name="ID" SessionField="ID" Type="Int32" />
        </SelectParameters>

        
        <UpdateParameters>
            <asp:Parameter Name="Notes" Type="String" />
            <asp:Parameter Name="TaskStatus" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
        
        
    </asp:SqlDataSource>


    


</asp:Content>