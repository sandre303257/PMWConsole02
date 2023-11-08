<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="TimeClock.aspx.vb" Inherits="PMWConsole02.TimeClock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <b>Employee: </b><asp:DropDownList ID="DdlEmployees" AppendDataBoundItems="True" runat="server" DataSourceID="SdsHourlyEmployees" DataTextField="Name" DataValueField="ID" AutoPostBack="True" Height="33px" Width="208px">
         <asp:ListItem Text="---select---" Value="0" />
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="LblStandardClockInLabel" runat="server" Text="Ordinary Clock In Time: " Font-Bold="true"></asp:Label>
    <asp:Label ID="LblStandardClockIn" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="LblStandardClockOutLabel" runat="server" Text="Ordinary Clock Out Time: " Font-Bold="true"></asp:Label>
    <asp:Label ID="LblStandardClockOut" runat="server" Text="Label"></asp:Label>
    
    
    <asp:Panel ID="PanelTimePunches" runat="server" Visible="true">
        <br />
        <b>Clocked in: </b><asp:Label ID="LblInTime" runat="server" Text="  "></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <b>Clocked out: </b><asp:Label ID="LblOutTime" runat="server" Text="  "></asp:Label>
        <br />
        <br />
        <asp:Button ID="BtnClockIn" runat="server" Text="Clock In" Height="75px" Width="130px" style="margin-top: 0px" Font-Size="Smaller" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnClockOut" runat="server" Height="75 px" Text="Clock Out" Width="130px" Font-Size="Smaller" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnClockInEarly" runat="server" Height="75px" style="margin-top: 0px" Text="Early Clock In" Width="130px" Font-Size="Smaller" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnClockOutLate" runat="server" Height="75px" Text="Late Clock Out" Width="130px" Font-Size="Smaller" />
        <br />
        <br />
        <b>Select A Date to Review:</b>&nbsp;<dx:ASPxDateEdit ID="DatePicker" runat="server" AutoPostBack="True" Theme="Default" Height="33px" Width="208px">
        </dx:ASPxDateEdit>
        <b>
        <br />
        Start Date: </b>
        <asp:Label ID="LblStartDate" runat="server" Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b>End Date: </b>
        <asp:Label ID="LblEndDate" runat="server" Text="Label"></asp:Label>


        <br />
        <dx:ASPxGridView ID="GridTimePunches" runat="server" AutoGenerateColumns="False" DataSourceID="SdsTimePunches" EnableTheming="True" KeyFieldName="ID" Theme="Default">
            <Settings ShowFooter="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsPopup>
                <FilterControl AutoUpdatePosition="False"></FilterControl>
            </SettingsPopup>
            <EditFormLayoutProperties ColCount="2" ColumnCount="2">
                <Items>
                    <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="date_worked" ColumnSpan="2">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="clock_in_1">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="clock_out_1">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="lunch">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="correction_hours">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="sick">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="vacation">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="holiday">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="comments" ColumnSpan="2">
                        <NestedControlStyle Wrap="True">
                        </NestedControlStyle>
                    </dx:GridViewColumnLayoutItem>
                    <dx:EditModeCommandLayoutItem ColSpan="2" ColumnSpan="2" HorizontalAlign="Right">
                    </dx:EditModeCommandLayoutItem>
                </Items>
            </EditFormLayoutProperties>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" Visible="False" VisibleIndex="12">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Sick Time" FieldName="sick" UnboundType="Decimal" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Vacation" FieldName="vacation" UnboundType="Decimal" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Holiday" FieldName="holiday" UnboundType="Decimal" VisibleIndex="9">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Lunch" FieldName="lunch" UnboundType="Decimal" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Correction" FieldName="correction_hours" UnboundType="Decimal" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Hours" ReadOnly="True" UnboundType="Decimal" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="employee_ID" UnboundType="Integer" Visible="False" VisibleIndex="0">
                    <SettingsHeaderFilter Mode="CheckedList">
                    </SettingsHeaderFilter>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTimeEditColumn Caption="In" FieldName="clock_in_1" UnboundType="Decimal" VisibleIndex="2" ReadOnly="True">
                    <PropertiesTimeEdit AllowNull="False" EditFormat="DateTime">
                    </PropertiesTimeEdit>
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings DisplayFormatString="t">
                        </DateRangePickerSettings>
                    </SettingsHeaderFilter>
                </dx:GridViewDataTimeEditColumn>
                <dx:GridViewDataTimeEditColumn Caption="Out" FieldName="clock_out_1" UnboundType="Decimal" VisibleIndex="3" ReadOnly="True">
                    <PropertiesTimeEdit AllowNull="False" EditFormat="DateTime">
                    </PropertiesTimeEdit>
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings DisplayFormatString="t">
                        </DateRangePickerSettings>
                    </SettingsHeaderFilter>
                </dx:GridViewDataTimeEditColumn>
                <dx:GridViewCommandColumn ButtonRenderMode="Button" ButtonType="Button" Caption=" " ShowEditButton="True" ShowUpdateButton="True" VisibleIndex="11">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataMemoColumn Caption="Comments" FieldName="comments" VisibleIndex="10">
                    <PropertiesMemoEdit>
                        <Style Wrap="True">
                        </Style>
                    </PropertiesMemoEdit>
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataDateColumn Caption="Date Worked" FieldName="date_worked" VisibleIndex="1" ReadOnly="True">
                </dx:GridViewDataDateColumn>
            </Columns>
            <TotalSummary>
                <dx:ASPxSummaryItem />
            </TotalSummary>
        </dx:ASPxGridView>
        <br />
        <br />
        <br />
    </asp:Panel>
    <asp:SqlDataSource ID="SdsHourlyEmployees" runat="server" ConnectionString="<%$ ConnectionStrings:PMW_ConsoleConnectionString2 %>" ProviderName="<%$ ConnectionStrings:PMW_ConsoleConnectionString2.ProviderName %>" SelectCommand="SELECT ID, Name, `Email Address`, Pay_type, Active, last_name, first_name, ClockInTime, ClockOutTime FROM employees WHERE (Pay_type = 'Hourly') AND (Active = 1) ORDER BY last_name, first_name"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SdsTimePunches" runat="server" ConnectionString="<%$ ConnectionStrings:PMW_ConsoleConnectionString2 %>" ProviderName="<%$ ConnectionStrings:PMW_ConsoleConnectionString2.ProviderName %>" 
        SelectCommand="SELECT ID, clock_in_1, clock_out_1, employee_ID, sick, vacation, holiday, lunch, correction_hours, date_worked, 
        ROUND(IF(clock_out_1=0, 0 + correction_hours, TIMESTAMPDIFF(MINUTE, clock_in_1, clock_out_1)/60 - lunch + correction_hours),1) AS Hours, comments FROM time_clock ORDER BY date_worked"
        UpdateCommand="UPDATE time_clock SET sick = @sick, vacation = @vacation, holiday = @holiday, lunch = @lunch, correction_hours = @correction_hours, comments = @comments WHERE (ID = @ID)"
        DeleteCommand="DELETE FROM time_clock WHERE (ID = @ID)">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="sick" Type="Double" />
            <asp:Parameter Name="vacation" Type="Double" />
            <asp:Parameter Name="holiday" Type="Decimal" />
            <asp:Parameter Name="lunch" Type="Double" />
            <asp:Parameter Name="correction_hours" Type="Double" />
            <asp:Parameter Name="comments" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>

    </asp:SqlDataSource>
    <br />
    <br />
    <br />




    </asp:Content>
