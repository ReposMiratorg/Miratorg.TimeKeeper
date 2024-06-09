using Microsoft.Extensions.Configuration;
using Storage.Data.EntitySigur;

namespace Miratorg.TimeKeeper.DataAccess.Contexts;

public class SigurDbContext : DbContext
{
    //public SigurDbContext(DbContextOptions<TimeKeeperDbContext> options) : base(options)
    //{
        
    //}

    public SigurDbContext()
    {
        this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        this.Database.SetCommandTimeout(TimeSpan.FromSeconds(1200));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Data Source=mhb-sql-2.agrohold.ru;Initial Catalog=Sigur; user id=UserServices; password=US#1#us; MultipleActiveResultSets=true;Encrypt=False;";

        optionsBuilder.UseSqlServer(connectionString);
        // optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Logs>(entity =>
        {
            entity.ToTable("logs");
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Accessrules>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("accessrules");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.AlkoAllowdrunka)
                .HasColumnName("ALKO_ALLOWDRUNKA");

            entity.Property(e => e.AlkoAllowdrunkb)
                .HasColumnName("ALKO_ALLOWDRUNKB");

            entity.Property(e => e.AlkoProba)
                .HasColumnName("ALKO_PROBA");

            entity.Property(e => e.AlkoProbb)
                .HasColumnName("ALKO_PROBB");

            entity.Property(e => e.AlkoThr)
                .HasColumnName("ALKO_THR");

            entity.Property(e => e.Boolparam1)
                .HasColumnName("BOOLPARAM1");

            entity.Property(e => e.Boolparam2)
                .HasColumnName("BOOLPARAM2");

            entity.Property(e => e.Boolparam3)
                .HasColumnName("BOOLPARAM3");

            entity.Property(e => e.Boolparam4)
                .HasColumnName("BOOLPARAM4");

            entity.Property(e => e.BreakType)
                .HasColumnName("BREAK_TYPE");

            entity.Property(e => e.CondSearchFor)
                .HasColumnName("COND_SEARCH_FOR");

            entity.Property(e => e.CondSearchWhere)
                .HasColumnName("COND_SEARCH_WHERE");

            entity.Property(e => e.CondType)
                .HasColumnName("COND_TYPE");

            entity.Property(e => e.Createdtime)
                .HasColumnName("CREATEDTIME");

            entity.Property(e => e.Deletedtime)
                .HasColumnName("DELETEDTIME");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.Enddate)
                .HasColumnName("ENDDATE");

            entity.Property(e => e.Extid)
                .HasColumnName("EXTID");

            entity.Property(e => e.Extsourceid)
                .HasColumnName("EXTSOURCEID");

            entity.Property(e => e.FaceiA)
                .HasColumnName("FACEI_A");

            entity.Property(e => e.FaceiB)
                .HasColumnName("FACEI_B");

            entity.Property(e => e.FacevA)
                .HasColumnName("FACEV_A");

            entity.Property(e => e.FacevB)
                .HasColumnName("FACEV_B");

            entity.Property(e => e.Guest)
                .HasColumnName("GUEST");

            entity.Property(e => e.LprpolicyA)
                .HasColumnName("LPRPOLICY_A");

            entity.Property(e => e.LprpolicyB)
                .HasColumnName("LPRPOLICY_B");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Nrules)
                .HasColumnName("NRULES");

            entity.Property(e => e.Nspecrules)
                .HasColumnName("NSPECRULES");

            entity.Property(e => e.OverrideWt)
                .HasColumnName("OVERRIDE_WT");

            entity.Property(e => e.ParkingTariffId)
                .HasColumnName("PARKING_TARIFF_ID");

            entity.Property(e => e.Poweridx)
                .HasColumnName("POWERIDX");

            entity.Property(e => e.Rule0)
                .HasColumnName("RULE0");

            entity.Property(e => e.Rule1)
                .HasColumnName("RULE1");

            entity.Property(e => e.Rule10)
                .HasColumnName("RULE10");

            entity.Property(e => e.Rule11)
                .HasColumnName("RULE11");

            entity.Property(e => e.Rule12)
                .HasColumnName("RULE12");

            entity.Property(e => e.Rule13)
                .HasColumnName("RULE13");

            entity.Property(e => e.Rule14)
                .HasColumnName("RULE14");

            entity.Property(e => e.Rule15)
                .HasColumnName("RULE15");

            entity.Property(e => e.Rule16)
                .HasColumnName("RULE16");

            entity.Property(e => e.Rule17)
                .HasColumnName("RULE17");

            entity.Property(e => e.Rule18)
                .HasColumnName("RULE18");

            entity.Property(e => e.Rule19)
                .HasColumnName("RULE19");

            entity.Property(e => e.Rule2)
                .HasColumnName("RULE2");

            entity.Property(e => e.Rule20)
                .HasColumnName("RULE20");

            entity.Property(e => e.Rule21)
                .HasColumnName("RULE21");

            entity.Property(e => e.Rule22)
                .HasColumnName("RULE22");

            entity.Property(e => e.Rule23)
                .HasColumnName("RULE23");

            entity.Property(e => e.Rule24)
                .HasColumnName("RULE24");

            entity.Property(e => e.Rule25)
                .HasColumnName("RULE25");

            entity.Property(e => e.Rule26)
                .HasColumnName("RULE26");

            entity.Property(e => e.Rule27)
                .HasColumnName("RULE27");

            entity.Property(e => e.Rule28)
                .HasColumnName("RULE28");

            entity.Property(e => e.Rule29)
                .HasColumnName("RULE29");

            entity.Property(e => e.Rule3)
                .HasColumnName("RULE3");

            entity.Property(e => e.Rule30)
                .HasColumnName("RULE30");

            entity.Property(e => e.Rule31)
                .HasColumnName("RULE31");

            entity.Property(e => e.Rule4)
                .HasColumnName("RULE4");

            entity.Property(e => e.Rule5)
                .HasColumnName("RULE5");

            entity.Property(e => e.Rule6)
                .HasColumnName("RULE6");

            entity.Property(e => e.Rule7)
                .HasColumnName("RULE7");

            entity.Property(e => e.Rule8)
                .HasColumnName("RULE8");

            entity.Property(e => e.Rule9)
                .HasColumnName("RULE9");

            entity.Property(e => e.Ruletype)
                .HasColumnName("RULETYPE");

            entity.Property(e => e.ServsyncExported)
                .HasColumnName("SERVSYNC_EXPORTED");

            entity.Property(e => e.Sideparam0)
                .HasColumnName("SIDEPARAM0");

            entity.Property(e => e.SpecDpassa)
                .HasColumnName("SPEC_DPASSA");

            entity.Property(e => e.SpecDpassb)
                .HasColumnName("SPEC_DPASSB");

            entity.Property(e => e.SpecDreadAction)
                .HasColumnName("SPEC_DREAD_ACTION");

            entity.Property(e => e.SpecEscortProb)
                .HasColumnName("SPEC_ESCORT_PROB");

            entity.Property(e => e.SpecEscortRuleid)
                .HasColumnName("SPEC_ESCORT_RULEID");

            entity.Property(e => e.SpecEscorta)
                .HasColumnName("SPEC_ESCORTA");

            entity.Property(e => e.SpecEscortb)
                .HasColumnName("SPEC_ESCORTB");

            entity.Property(e => e.SpecExptimecollect)
                .HasColumnName("SPEC_EXPTIMECOLLECT");

            entity.Property(e => e.SpecExtreadera)
                .HasColumnName("SPEC_EXTREADERA");

            entity.Property(e => e.SpecExtreaderb)
                .HasColumnName("SPEC_EXTREADERB");

            entity.Property(e => e.SpecOpreqa)
                .HasColumnName("SPEC_OPREQA");

            entity.Property(e => e.SpecOpreqb)
                .HasColumnName("SPEC_OPREQB");

            entity.Property(e => e.SpecPayitema)
                .HasColumnName("SPEC_PAYITEMA");

            entity.Property(e => e.SpecPayitemb)
                .HasColumnName("SPEC_PAYITEMB");

            entity.Property(e => e.SpecReqpina)
                .HasColumnName("SPEC_REQPINA");

            entity.Property(e => e.SpecReqpinb)
                .HasColumnName("SPEC_REQPINB");

            entity.Property(e => e.Specdate0)
                .HasColumnName("SPECDATE0");

            entity.Property(e => e.Specdate1)
                .HasColumnName("SPECDATE1");

            entity.Property(e => e.Specdate10)
                .HasColumnName("SPECDATE10");

            entity.Property(e => e.Specdate11)
                .HasColumnName("SPECDATE11");

            entity.Property(e => e.Specdate12)
                .HasColumnName("SPECDATE12");

            entity.Property(e => e.Specdate13)
                .HasColumnName("SPECDATE13");

            entity.Property(e => e.Specdate14)
                .HasColumnName("SPECDATE14");

            entity.Property(e => e.Specdate15)
                .HasColumnName("SPECDATE15");

            entity.Property(e => e.Specdate16)
                .HasColumnName("SPECDATE16");

            entity.Property(e => e.Specdate17)
                .HasColumnName("SPECDATE17");

            entity.Property(e => e.Specdate18)
                .HasColumnName("SPECDATE18");

            entity.Property(e => e.Specdate19)
                .HasColumnName("SPECDATE19");

            entity.Property(e => e.Specdate2)
                .HasColumnName("SPECDATE2");

            entity.Property(e => e.Specdate20)
                .HasColumnName("SPECDATE20");

            entity.Property(e => e.Specdate21)
                .HasColumnName("SPECDATE21");

            entity.Property(e => e.Specdate22)
                .HasColumnName("SPECDATE22");

            entity.Property(e => e.Specdate23)
                .HasColumnName("SPECDATE23");

            entity.Property(e => e.Specdate24)
                .HasColumnName("SPECDATE24");

            entity.Property(e => e.Specdate25)
                .HasColumnName("SPECDATE25");

            entity.Property(e => e.Specdate26)
                .HasColumnName("SPECDATE26");

            entity.Property(e => e.Specdate27)
                .HasColumnName("SPECDATE27");

            entity.Property(e => e.Specdate28)
                .HasColumnName("SPECDATE28");

            entity.Property(e => e.Specdate29)
                .HasColumnName("SPECDATE29");

            entity.Property(e => e.Specdate3)
                .HasColumnName("SPECDATE3");

            entity.Property(e => e.Specdate30)
                .HasColumnName("SPECDATE30");

            entity.Property(e => e.Specdate31)
                .HasColumnName("SPECDATE31");

            entity.Property(e => e.Specdate4)
                .HasColumnName("SPECDATE4");

            entity.Property(e => e.Specdate5)
                .HasColumnName("SPECDATE5");

            entity.Property(e => e.Specdate6)
                .HasColumnName("SPECDATE6");

            entity.Property(e => e.Specdate7)
                .HasColumnName("SPECDATE7");

            entity.Property(e => e.Specdate8)
                .HasColumnName("SPECDATE8");

            entity.Property(e => e.Specdate9)
                .HasColumnName("SPECDATE9");

            entity.Property(e => e.Specrule0)
                .HasColumnName("SPECRULE0");

            entity.Property(e => e.Specrule1)
                .HasColumnName("SPECRULE1");

            entity.Property(e => e.Specrule10)
                .HasColumnName("SPECRULE10");

            entity.Property(e => e.Specrule11)
                .HasColumnName("SPECRULE11");

            entity.Property(e => e.Specrule12)
                .HasColumnName("SPECRULE12");

            entity.Property(e => e.Specrule13)
                .HasColumnName("SPECRULE13");

            entity.Property(e => e.Specrule14)
                .HasColumnName("SPECRULE14");

            entity.Property(e => e.Specrule15)
                .HasColumnName("SPECRULE15");

            entity.Property(e => e.Specrule16)
                .HasColumnName("SPECRULE16");

            entity.Property(e => e.Specrule17)
                .HasColumnName("SPECRULE17");

            entity.Property(e => e.Specrule18)
                .HasColumnName("SPECRULE18");

            entity.Property(e => e.Specrule19)
                .HasColumnName("SPECRULE19");

            entity.Property(e => e.Specrule2)
                .HasColumnName("SPECRULE2");

            entity.Property(e => e.Specrule20)
                .HasColumnName("SPECRULE20");

            entity.Property(e => e.Specrule21)
                .HasColumnName("SPECRULE21");

            entity.Property(e => e.Specrule22)
                .HasColumnName("SPECRULE22");

            entity.Property(e => e.Specrule23)
                .HasColumnName("SPECRULE23");

            entity.Property(e => e.Specrule24)
                .HasColumnName("SPECRULE24");

            entity.Property(e => e.Specrule25)
                .HasColumnName("SPECRULE25");

            entity.Property(e => e.Specrule26)
                .HasColumnName("SPECRULE26");

            entity.Property(e => e.Specrule27)
                .HasColumnName("SPECRULE27");

            entity.Property(e => e.Specrule28)
                .HasColumnName("SPECRULE28");

            entity.Property(e => e.Specrule29)
                .HasColumnName("SPECRULE29");

            entity.Property(e => e.Specrule3)
                .HasColumnName("SPECRULE3");

            entity.Property(e => e.Specrule30)
                .HasColumnName("SPECRULE30");

            entity.Property(e => e.Specrule31)
                .HasColumnName("SPECRULE31");

            entity.Property(e => e.Specrule4)
                .HasColumnName("SPECRULE4");

            entity.Property(e => e.Specrule5)
                .HasColumnName("SPECRULE5");

            entity.Property(e => e.Specrule6)
                .HasColumnName("SPECRULE6");

            entity.Property(e => e.Specrule7)
                .HasColumnName("SPECRULE7");

            entity.Property(e => e.Specrule8)
                .HasColumnName("SPECRULE8");

            entity.Property(e => e.Specrule9)
                .HasColumnName("SPECRULE9");

            entity.Property(e => e.Startdate)
                .HasColumnName("STARTDATE");

            entity.Property(e => e.Status)
                .HasColumnName("STATUS");

            entity.Property(e => e.Stdweekmode)
                .HasColumnName("STDWEEKMODE");

            entity.Property(e => e.Wtmode)
                .HasColumnName("WTMODE");
        });

        modelBuilder.Entity<Addomains>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("addomains");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Addr)
                .HasColumnName("ADDR");

            entity.Property(e => e.DomainName)
                .HasColumnName("DOMAIN_NAME");

            entity.Property(e => e.Login)
                .HasColumnName("LOGIN");

            entity.Property(e => e.Pass)
                .HasColumnName("PASS");
        });

        modelBuilder.Entity<Adobjzones>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("adobjzones");

            entity.Property(e => e.Zoneid)
                .HasColumnName("ZONEID");

            entity.Property(e => e.Objid)
                .HasColumnName("OBJID");
        });

        modelBuilder.Entity<Alarmlines>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("alarmlines");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Ap)
                .HasColumnName("AP");

            entity.Property(e => e.CamParam1)
                .HasColumnName("CAM_PARAM1");

            entity.Property(e => e.CamParam2)
                .HasColumnName("CAM_PARAM2");

            entity.Property(e => e.CamParam3)
                .HasColumnName("CAM_PARAM3");

            entity.Property(e => e.CamParam4)
                .HasColumnName("CAM_PARAM4");

            entity.Property(e => e.CamServerId)
                .HasColumnName("CAM_SERVER_ID");

            entity.Property(e => e.Linetype)
                .HasColumnName("LINETYPE");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Number)
                .HasColumnName("NUMBER");

            entity.Property(e => e.OrderIdx)
                .HasColumnName("ORDER_IDX");

            entity.Property(e => e.ParentId)
                .HasColumnName("PARENT_ID");

            entity.Property(e => e.Rubezhtype)
                .HasColumnName("RUBEZHTYPE");

            entity.Property(e => e.Spnxtype)
                .HasColumnName("SPNXTYPE");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.Zone)
                .HasColumnName("ZONE");
        });

        modelBuilder.Entity<Alarmlog>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("alarmlog");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Framets)
                .HasColumnName("FRAMETS");

            entity.Property(e => e.Lineid)
                .HasColumnName("LINEID");

            entity.Property(e => e.Logtime)
                .HasColumnName("LOGTIME");

            entity.Property(e => e.Newstate)
                .HasColumnName("NEWSTATE");
        });

        modelBuilder.Entity<Almbinduser>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("almbinduser");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");

            entity.Property(e => e.AlmId)
                .HasColumnName("ALM_ID");
        });

        modelBuilder.Entity<AzDevicesdata>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("az_devicesdata");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Attmode)
                .HasColumnName("ATTMODE");

            entity.Property(e => e.Codekey)
                .HasColumnName("CODEKEY");

            entity.Property(e => e.Dirtybit)
                .HasColumnName("DIRTYBIT");

            entity.Property(e => e.Fp0)
                .HasColumnName("FP0");

            entity.Property(e => e.Fp1)
                .HasColumnName("FP1");

            entity.Property(e => e.Fp2)
                .HasColumnName("FP2");

            entity.Property(e => e.Fp3)
                .HasColumnName("FP3");

            entity.Property(e => e.Fp4)
                .HasColumnName("FP4");

            entity.Property(e => e.Fp5)
                .HasColumnName("FP5");

            entity.Property(e => e.Fp6)
                .HasColumnName("FP6");

            entity.Property(e => e.Fp7)
                .HasColumnName("FP7");

            entity.Property(e => e.Fp8)
                .HasColumnName("FP8");

            entity.Property(e => e.Fp9)
                .HasColumnName("FP9");

            entity.Property(e => e.Ip)
                .HasColumnName("IP");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Port)
                .HasColumnName("PORT");

            entity.Property(e => e.Pwd)
                .HasColumnName("PWD");

            entity.Property(e => e.Userid)
                .HasColumnName("USERID");
        });

        modelBuilder.Entity<Badgeitems>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("badgeitems");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Align)
                .HasColumnName("ALIGN");

            entity.Property(e => e.Badgeid)
                .HasColumnName("BADGEID");

            entity.Property(e => e.BarcodeType)
                .HasColumnName("BARCODE_TYPE");

            entity.Property(e => e.Bindata)
                .HasColumnName("BINDATA");

            entity.Property(e => e.CondSearchFor)
                .HasColumnName("COND_SEARCH_FOR");

            entity.Property(e => e.CondSearchWhere)
                .HasColumnName("COND_SEARCH_WHERE");

            entity.Property(e => e.CondType)
                .HasColumnName("COND_TYPE");

            entity.Property(e => e.Eba)
                .HasColumnName("EBA");

            entity.Property(e => e.FontBold)
                .HasColumnName("FONT_BOLD");

            entity.Property(e => e.FontColorB)
                .HasColumnName("FONT_COLOR_B");

            entity.Property(e => e.FontColorG)
                .HasColumnName("FONT_COLOR_G");

            entity.Property(e => e.FontColorR)
                .HasColumnName("FONT_COLOR_R");

            entity.Property(e => e.FontDef)
                .HasColumnName("FONT_DEF");

            entity.Property(e => e.FontItalic)
                .HasColumnName("FONT_ITALIC");

            entity.Property(e => e.FontMin)
                .HasColumnName("FONT_MIN");

            entity.Property(e => e.FontName)
                .HasColumnName("FONT_NAME");

            entity.Property(e => e.Height)
                .HasColumnName("HEIGHT");

            entity.Property(e => e.ImgName)
                .HasColumnName("IMG_NAME");

            entity.Property(e => e.ImgType)
                .HasColumnName("IMG_TYPE");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Param1)
                .HasColumnName("PARAM1");

            entity.Property(e => e.PhotoCircle)
                .HasColumnName("PHOTO_CIRCLE");

            entity.Property(e => e.Rotate)
                .HasColumnName("ROTATE");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.Width)
                .HasColumnName("WIDTH");

            entity.Property(e => e.Xpos)
                .HasColumnName("XPOS");

            entity.Property(e => e.Ypos)
                .HasColumnName("YPOS");
        });

        modelBuilder.Entity<Badges2>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("badges2");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Binbackground)
                .HasColumnName("BINBACKGROUND");

            entity.Property(e => e.Height)
                .HasColumnName("HEIGHT");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.Width)
                .HasColumnName("WIDTH");
        });

        modelBuilder.Entity<BasipDevicesdata>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("basip_devicesdata");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.CardNumber)
                .HasColumnName("CARD_NUMBER");

            entity.Property(e => e.DeviceId)
                .HasColumnName("DEVICE_ID");

            entity.Property(e => e.Dirtybit)
                .HasColumnName("DIRTYBIT");
        });

        modelBuilder.Entity<BioTemplates>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("bio_templates");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.Template)
                .HasColumnName("TEMPLATE");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<BleRegtokens>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("ble_regtokens");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Createdtime)
                .HasColumnName("CREATEDTIME");

            entity.Property(e => e.Objid)
                .HasColumnName("OBJID");

            entity.Property(e => e.Objtype)
                .HasColumnName("OBJTYPE");

            entity.Property(e => e.Regtime)
                .HasColumnName("REGTIME");

            entity.Property(e => e.Token)
                .HasColumnName("TOKEN");
        });

        modelBuilder.Entity<BsDevicesdata>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("bs_devicesdata");

            entity.HasIndex(e => e.Id)
                .HasName("ID");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.BsType)
                .HasColumnName("BS_TYPE");

            entity.Property(e => e.Codekey)
                .HasColumnName("CODEKEY");

            entity.Property(e => e.Dirtybit)
                .HasColumnName("DIRTYBIT");

            entity.Property(e => e.Ip)
                .HasColumnName("IP");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Port)
                .HasColumnName("PORT");

            entity.Property(e => e.TemplateId)
                .HasColumnName("TEMPLATE_ID");
        });

        modelBuilder.Entity<Cctvservers>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("cctvservers");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.FgMaxfps)
                .HasColumnName("FG_MAXFPS");

            entity.Property(e => e.FgMaxframeheight)
                .HasColumnName("FG_MAXFRAMEHEIGHT");

            entity.Property(e => e.FgMaxframewidth)
                .HasColumnName("FG_MAXFRAMEWIDTH");

            entity.Property(e => e.FgOnvifName)
                .HasColumnName("FG_ONVIF_NAME");

            entity.Property(e => e.FgOnvifToken)
                .HasColumnName("FG_ONVIF_TOKEN");

            entity.Property(e => e.FgOnvifType)
                .HasColumnName("FG_ONVIF_TYPE");

            entity.Property(e => e.FgPhoto)
                .HasColumnName("FG_PHOTO");

            entity.Property(e => e.FgPostseconds)
                .HasColumnName("FG_POSTSECONDS");

            entity.Property(e => e.FgPreseconds)
                .HasColumnName("FG_PRESECONDS");

            entity.Property(e => e.FgSetGuestPhoto)
                .HasColumnName("FG_SET_GUEST_PHOTO");

            entity.Property(e => e.FgVideo)
                .HasColumnName("FG_VIDEO");

            entity.Property(e => e.Ip)
                .HasColumnName("IP");

            entity.Property(e => e.Login)
                .HasColumnName("LOGIN");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Param1)
                .HasColumnName("PARAM1");

            entity.Property(e => e.Param2)
                .HasColumnName("PARAM2");

            entity.Property(e => e.Param3)
                .HasColumnName("PARAM3");

            entity.Property(e => e.Param4)
                .HasColumnName("PARAM4");

            entity.Property(e => e.Param5)
                .HasColumnName("PARAM5");

            entity.Property(e => e.Param6)
                .HasColumnName("PARAM6");

            entity.Property(e => e.Param7)
                .HasColumnName("PARAM7");

            entity.Property(e => e.Param8)
                .HasColumnName("PARAM8");

            entity.Property(e => e.Passwd)
                .HasColumnName("PASSWD");

            entity.Property(e => e.Port)
                .HasColumnName("PORT");

            entity.Property(e => e.SsFrameDate)
                .HasColumnName("SS_FRAME_DATE");

            entity.Property(e => e.SsFrameDescriptorIdx)
                .HasColumnName("SS_FRAME_DESCRIPTOR_IDX");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.Url)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Devbindings>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.EmpId, e.DevId });
            //.HasName("PRIMARY");

            entity.ToTable("devbindings");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.DevId)
                .HasColumnName("DEV_ID");
        });

        modelBuilder.Entity<Devbinduser>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.UserId, e.DevId });
            //.HasName("PRIMARY");

            entity.ToTable("devbinduser");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");

            entity.Property(e => e.DevId)
                .HasColumnName("DEV_ID");
        });

        modelBuilder.Entity<DevempidxData>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Mapid, e.Empid, e.Empidx });
            //.HasName("PRIMARY");

            entity.ToTable("devempidx_data");

            entity.Property(e => e.Mapid)
                .HasColumnName("MAPID");

            entity.Property(e => e.Empid)
                .HasColumnName("EMPID");

            entity.Property(e => e.Empidx)
                .HasColumnName("EMPIDX");
        });

        modelBuilder.Entity<DevempidxMap>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Devid);
            entity.Property(e => e.Devid).ValueGeneratedNever();

            entity.ToTable("devempidx_map");

            entity.Property(e => e.Devid)
                .HasColumnName("DEVID");

            entity.Property(e => e.Mapid)
                .HasColumnName("MAPID");
        });

        modelBuilder.Entity<Deviceconfs>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("deviceconfs");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Confdata)
                .HasColumnName("CONFDATA");

            entity.Property(e => e.Gatesm)
                .HasColumnName("GATESM");

            entity.Property(e => e.Iocount)
                .HasColumnName("IOCOUNT");

            entity.Property(e => e.Iorec)
                .HasColumnName("IOREC");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Devices>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("devices");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.AplOn)
                .HasColumnName("APL_ON");

            entity.Property(e => e.AplTarget)
                .HasColumnName("APL_TARGET");

            entity.Property(e => e.Area)
                .HasColumnName("AREA");

            entity.Property(e => e.BioaIp)
                .HasColumnName("BIOA_IP");

            entity.Property(e => e.BioaPort)
                .HasColumnName("BIOA_PORT");

            entity.Property(e => e.BioaType)
                .HasColumnName("BIOA_TYPE");

            entity.Property(e => e.BiobIp)
                .HasColumnName("BIOB_IP");

            entity.Property(e => e.BiobPort)
                .HasColumnName("BIOB_PORT");

            entity.Property(e => e.BiobType)
                .HasColumnName("BIOB_TYPE");

            entity.Property(e => e.CamaFaceiEnabled)
                .HasColumnName("CAMA_FACEI_ENABLED");

            entity.Property(e => e.CamaFacevEnabled)
                .HasColumnName("CAMA_FACEV_ENABLED");

            entity.Property(e => e.CamaLpr)
                .HasColumnName("CAMA_LPR");

            entity.Property(e => e.CamaParam1)
                .HasColumnName("CAMA_PARAM1");

            entity.Property(e => e.CamaParam2)
                .HasColumnName("CAMA_PARAM2");

            entity.Property(e => e.CamaParam3)
                .HasColumnName("CAMA_PARAM3");

            entity.Property(e => e.CamaParam4)
                .HasColumnName("CAMA_PARAM4");

            entity.Property(e => e.CamaServerId)
                .HasColumnName("CAMA_SERVER_ID");

            entity.Property(e => e.CambFaceiEnabled)
                .HasColumnName("CAMB_FACEI_ENABLED");

            entity.Property(e => e.CambFacevEnabled)
                .HasColumnName("CAMB_FACEV_ENABLED");

            entity.Property(e => e.CambLpr)
                .HasColumnName("CAMB_LPR");

            entity.Property(e => e.CambParam1)
                .HasColumnName("CAMB_PARAM1");

            entity.Property(e => e.CambParam2)
                .HasColumnName("CAMB_PARAM2");

            entity.Property(e => e.CambParam3)
                .HasColumnName("CAMB_PARAM3");

            entity.Property(e => e.CambParam4)
                .HasColumnName("CAMB_PARAM4");

            entity.Property(e => e.CambServerId)
                .HasColumnName("CAMB_SERVER_ID");

            entity.Property(e => e.Controlsperimeter)
                .HasColumnName("CONTROLSPERIMETER");

            entity.Property(e => e.Disabled)
                .HasColumnName("DISABLED");

            entity.Property(e => e.Extid)
                .HasColumnName("EXTID");

            entity.Property(e => e.Extsourceid)
                .HasColumnName("EXTSOURCEID");

            entity.Property(e => e.Gatesm)
                .HasColumnName("GATESM");

            entity.Property(e => e.GuestCloseonexit)
                .HasColumnName("GUEST_CLOSEONEXIT");

            entity.Property(e => e.Hwconf)
                .HasColumnName("HWCONF");

            entity.Property(e => e.Linkparam1)
                .HasColumnName("LINKPARAM1");

            entity.Property(e => e.Linkparam2)
                .HasColumnName("LINKPARAM2");

            entity.Property(e => e.Linkparam3)
                .HasColumnName("LINKPARAM3");

            entity.Property(e => e.Linkparam4)
                .HasColumnName("LINKPARAM4");

            entity.Property(e => e.Linkparam5)
                .HasColumnName("LINKPARAM5");

            entity.Property(e => e.Linktype)
                .HasColumnName("LINKTYPE");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.NfcExptimeCheck)
                .HasColumnName("NFC_EXPTIME_CHECK");

            entity.Property(e => e.OrderIdx)
                .HasColumnName("ORDER_IDX");

            entity.Property(e => e.ParentId)
                .HasColumnName("PARENT_ID");

            entity.Property(e => e.Pinmagica)
                .HasColumnName("PINMAGICA");

            entity.Property(e => e.Pinmagicb)
                .HasColumnName("PINMAGICB");

            entity.Property(e => e.Sms)
                .HasColumnName("SMS");

            entity.Property(e => e.TimezoneId)
                .HasColumnName("TIMEZONE_ID");

            entity.Property(e => e.TrueipLasteventId)
                .HasColumnName("TRUEIP_LASTEVENT_ID");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.Zonea)
                .HasColumnName("ZONEA");

            entity.Property(e => e.Zoneb)
                .HasColumnName("ZONEB");
        });

        modelBuilder.Entity<Devrulebindings>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.DevId, e.RuleId });

            entity.ToTable("devrulebindings");

            entity.Property(e => e.DevId)
                .HasColumnName("DEV_ID");

            entity.Property(e => e.RuleId)
                .HasColumnName("RULE_ID");
        });

        modelBuilder.Entity<ElDevicesdata>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("el_devicesdata");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Codekey)
                .HasColumnName("CODEKEY");

            entity.Property(e => e.Host)
                .HasColumnName("HOST");

            entity.Property(e => e.Port)
                .HasColumnName("PORT");

            entity.Property(e => e.TemplateId)
                .HasColumnName("TEMPLATE_ID");
        });

        modelBuilder.Entity<Emailqueue>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("emailqueue");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Body)
                .HasColumnName("BODY");

            entity.Property(e => e.CcCsv)
                .HasColumnName("CC_CSV");

            entity.Property(e => e.Pushtime)
                .HasColumnName("PUSHTIME");

            entity.Property(e => e.Subject)
                .HasColumnName("SUBJECT");
            entity.Property(e => e.ToCsv)
                .HasColumnName("TO_CSV");
        });

        modelBuilder.Entity<Evaction>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("evaction");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Param1)
                .HasColumnName("PARAM1");

            entity.Property(e => e.Param2)
                .HasColumnName("PARAM2");

            entity.Property(e => e.Param3)
                .HasColumnName("PARAM3");

            entity.Property(e => e.Param4)
                .HasColumnName("PARAM4");

            entity.Property(e => e.Param5)
                .HasColumnName("PARAM5");

            entity.Property(e => e.Param6)
                .HasColumnName("PARAM6");

            entity.Property(e => e.Param7)
                .HasColumnName("PARAM7");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<Evactionap>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Action, e.Ap });

            entity.ToTable("evactionap");

            entity.Property(e => e.Action)
                .HasColumnName("ACTION");

            entity.Property(e => e.Ap)
                .HasColumnName("AP");
        });

        modelBuilder.Entity<Evactionemp>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("evactionemp");

            entity.Property(e => e.Action)
                .HasColumnName("ACTION");

            entity.Property(e => e.Emp)
                .HasColumnName("EMP");
        });

        modelBuilder.Entity<Evactionzone>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("evactionzone");

            entity.Property(e => e.Action)
                .HasColumnName("ACTION");

            entity.Property(e => e.Zone)
                .HasColumnName("ZONE");
        });

        modelBuilder.Entity<Evcond>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("evcond");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Lastfiretime)
                .HasColumnName("LASTFIRETIME");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Param1)
                .HasColumnName("PARAM1");

            entity.Property(e => e.Param2)
                .HasColumnName("PARAM2");

            entity.Property(e => e.Param3)
                .HasColumnName("PARAM3");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<Evcondaction>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Cond, e.Action });

            entity.ToTable("evcondaction");

            entity.Property(e => e.Cond)
                .HasColumnName("COND");

            entity.Property(e => e.Action)
                .HasColumnName("ACTION");
        });

        modelBuilder.Entity<Evcondalmlines>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Cond, e.Lineid });

            entity.ToTable("evcondalmlines");

            entity.Property(e => e.Cond)
                .HasColumnName("COND");

            entity.Property(e => e.Lineid)
                .HasColumnName("LINEID");
        });

        modelBuilder.Entity<Evcondap>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Cond, e.Ap });

            entity.ToTable("evcondap");

            entity.Property(e => e.Cond)
                .HasColumnName("COND");

            entity.Property(e => e.Ap)
                .HasColumnName("AP");
        });

        modelBuilder.Entity<Evcondemp>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Cond, e.Emp });

            entity.ToTable("evcondemp");

            entity.Property(e => e.Cond)
                .HasColumnName("COND");

            entity.Property(e => e.Emp)
                .HasColumnName("EMP");
        });

        modelBuilder.Entity<Evcondparamset>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Cond, e.Paramvalue });

            entity.ToTable("evcondparamset");

            entity.Property(e => e.Cond)
                .HasColumnName("COND");

            entity.Property(e => e.Paramvalue)
                .HasColumnName("PARAMVALUE");
        });

        modelBuilder.Entity<Evcondsch>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Condid);
            entity.Property(e => e.Condid).ValueGeneratedNever();

            entity.ToTable("evcondsch");

            entity.Property(e => e.Condid)
                .HasColumnName("CONDID");

            entity.Property(e => e.Daynumber)
                .HasColumnName("DAYNUMBER");

            entity.Property(e => e.Time)
                .HasColumnName("TIME");
        });

        modelBuilder.Entity<Execqueue>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("execqueue");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Cmd)
                .HasColumnName("CMD");

            entity.Property(e => e.Pushtime)
                .HasColumnName("PUSHTIME");
        });

        modelBuilder.Entity<ExtdbsyncQueries>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("extdbsync_queries");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Filter)
                .HasColumnName("FILTER");

            entity.Property(e => e.SourceId)
                .HasColumnName("SOURCE_ID");

            entity.Property(e => e.Text)
                .HasColumnName("TEXT");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<ExtdbsyncQueriesresults>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Queryid, e.Type, e.Typeparam });

            entity.ToTable("extdbsync_queriesresults");

            entity.Property(e => e.Queryid)
                .HasColumnName("QUERYID");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.Typeparam)
                .HasColumnName("TYPEPARAM");

            entity.Property(e => e.Colname)
                .HasColumnName("COLNAME");

            entity.Property(e => e.PostprocParam1)
                .HasColumnName("POSTPROC_PARAM1");

            entity.Property(e => e.PostprocType)
                .HasColumnName("POSTPROC_TYPE");
        });

        modelBuilder.Entity<ExtdbsyncSources>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("extdbsync_sources");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.AdDomainId)
                .HasColumnName("AD_DOMAIN_ID");

            entity.Property(e => e.AdPath)
                .HasColumnName("AD_PATH");

            entity.Property(e => e.AdSyncAccdisabled)
                .HasColumnName("AD_SYNC_ACCDISABLED");

            entity.Property(e => e.AdSyncDepartment)
                .HasColumnName("AD_SYNC_DEPARTMENT");

            entity.Property(e => e.AdSyncTabid)
                .HasColumnName("AD_SYNC_TABID");

            entity.Property(e => e.DelClrdevbindings)
                .HasColumnName("DEL_CLRDEVBINDINGS");

            entity.Property(e => e.DelDep)
                .HasColumnName("DEL_DEP");

            entity.Property(e => e.DelPolicy)
                .HasColumnName("DEL_POLICY");

            entity.Property(e => e.DelSetexptime)
                .HasColumnName("DEL_SETEXPTIME");

            entity.Property(e => e.DeptsDelimiter)
                .HasColumnName("DEPTS_DELIMITER");

            entity.Property(e => e.Enabled)
                .HasColumnName("ENABLED");

            entity.Property(e => e.KeysDelimiter)
                .HasColumnName("KEYS_DELIMITER");

            entity.Property(e => e.LdapAuthHandle)
                .HasColumnName("LDAP_AUTH_HANDLE");

            entity.Property(e => e.LdapAuthPass)
                .HasColumnName("LDAP_AUTH_PASS");

            entity.Property(e => e.LdapForceV3)
                .HasColumnName("LDAP_FORCE_V3");

            entity.Property(e => e.LdapUrl)
                .HasColumnName("LDAP_URL");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.OnecOrg)
                .HasColumnName("ONEC_ORG");

            entity.Property(e => e.OnecServerId)
                .HasColumnName("ONEC_SERVER_ID");

            entity.Property(e => e.Period)
                .HasColumnName("PERIOD");

            entity.Property(e => e.RulesDelimiter)
                .HasColumnName("RULES_DELIMITER");

            entity.Property(e => e.ServsyncAccessrulesIn)
                .HasColumnName("SERVSYNC_ACCESSRULES_IN");

            entity.Property(e => e.ServsyncAccessrulesOut)
                .HasColumnName("SERVSYNC_ACCESSRULES_OUT");

            entity.Property(e => e.ServsyncDevicesIn)
                .HasColumnName("SERVSYNC_DEVICES_IN");

            entity.Property(e => e.ServsyncDevicesOut)
                .HasColumnName("SERVSYNC_DEVICES_OUT");

            entity.Property(e => e.ServsyncKey)
                .HasColumnName("SERVSYNC_KEY");

            entity.Property(e => e.ServsyncLogsIn)
                .HasColumnName("SERVSYNC_LOGS_IN");

            entity.Property(e => e.ServsyncLogsInLastid)
                .HasColumnName("SERVSYNC_LOGS_IN_LASTID");

            entity.Property(e => e.ServsyncLogsOut)
                .HasColumnName("SERVSYNC_LOGS_OUT");

            entity.Property(e => e.ServsyncLogsOutLastid)
                .HasColumnName("SERVSYNC_LOGS_OUT_LASTID");

            entity.Property(e => e.ServsyncOfflineEnabled)
                .HasColumnName("SERVSYNC_OFFLINE_ENABLED");

            entity.Property(e => e.ServsyncOfflineExchangeDir)
                .HasColumnName("SERVSYNC_OFFLINE_EXCHANGE_DIR");

            entity.Property(e => e.ServsyncOfflineLastTs)
                .HasColumnName("SERVSYNC_OFFLINE_LAST_TS");

            entity.Property(e => e.ServsyncOfflinePeriod)
                .HasColumnName("SERVSYNC_OFFLINE_PERIOD");

            entity.Property(e => e.ServsyncOnlineEnabled)
                .HasColumnName("SERVSYNC_ONLINE_ENABLED");

            entity.Property(e => e.ServsyncOnlineMode)
                .HasColumnName("SERVSYNC_ONLINE_MODE");

            entity.Property(e => e.ServsyncOnlinePeerhost)
                .HasColumnName("SERVSYNC_ONLINE_PEERHOST");

            entity.Property(e => e.ServsyncOnlinePeerport)
                .HasColumnName("SERVSYNC_ONLINE_PEERPORT");

            entity.Property(e => e.ServsyncOnlinePeriod)
                .HasColumnName("SERVSYNC_ONLINE_PERIOD");

            entity.Property(e => e.ServsyncPeeruuid)
                .HasColumnName("SERVSYNC_PEERUUID");

            entity.Property(e => e.ServsyncPersonalIn)
                .HasColumnName("SERVSYNC_PERSONAL_IN");

            entity.Property(e => e.ServsyncPersonalOut)
                .HasColumnName("SERVSYNC_PERSONAL_OUT");

            entity.Property(e => e.ServsyncPhotoIn)
                .HasColumnName("SERVSYNC_PHOTO_IN");

            entity.Property(e => e.ServsyncPhotoOut)
                .HasColumnName("SERVSYNC_PHOTO_OUT");

            entity.Property(e => e.ServsyncRulebindingsIn)
                .HasColumnName("SERVSYNC_RULEBINDINGS_IN");

            entity.Property(e => e.ServsyncRulebindingsOut)
                .HasColumnName("SERVSYNC_RULEBINDINGS_OUT");

            entity.Property(e => e.ServsyncStatus)
                .HasColumnName("SERVSYNC_STATUS");

            entity.Property(e => e.ServsyncStatusDetails)
                .HasColumnName("SERVSYNC_STATUS_DETAILS");

            entity.Property(e => e.SqlExtdbver)
                .HasColumnName("SQL_EXTDBVER");

            entity.Property(e => e.SqlLogLastid)
                .HasColumnName("SQL_LOG_LASTID");

            entity.Property(e => e.SqlLogUsedeny)
                .HasColumnName("SQL_LOG_USEDENY");

            entity.Property(e => e.SqlLogUsenoid)
                .HasColumnName("SQL_LOG_USENOID");

            entity.Property(e => e.SqlLogUsepass)
                .HasColumnName("SQL_LOG_USEPASS");

            entity.Property(e => e.SqlOdbcstring)
                .HasColumnName("SQL_ODBCSTRING");

            entity.Property(e => e.SyncPhotos)
                .HasColumnName("SYNC_PHOTOS");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<Floorbinduser>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.UserId, e.FloorId });

            entity.ToTable("floorbinduser");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");

            entity.Property(e => e.FloorId)
                .HasColumnName("FLOOR_ID");
        });

        modelBuilder.Entity<Frames>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("frames");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Cameraid)
                .HasColumnName("CAMERAID");

            entity.Property(e => e.Raster)
                .HasColumnName("RASTER");

            entity.Property(e => e.Timestamp)
                .HasColumnName("TIMESTAMP");
        });

        modelBuilder.Entity<Gsmsmsqueue>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("gsmsmsqueue");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Pushtime)
                .HasColumnName("PUSHTIME");

            entity.Property(e => e.Smstext)
                .HasColumnName("SMSTEXT");

            entity.Property(e => e.Targetnumber)
                .HasColumnName("TARGETNUMBER");
        });

        modelBuilder.Entity<Gstappl>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("gstappl");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.CometoId)
                .HasColumnName("COMETO_ID");

            entity.Property(e => e.Comment)
                .HasColumnName("COMMENT");

            entity.Property(e => e.Createdtime)
                .HasColumnName("CREATEDTIME");

            entity.Property(e => e.OwnerId)
                .HasColumnName("OWNER_ID");

            entity.Property(e => e.PeriodBegin)
                .HasColumnName("PERIOD_BEGIN");

            entity.Property(e => e.PeriodEnd)
                .HasColumnName("PERIOD_END");

            entity.Property(e => e.Sideparam0)
                .HasColumnName("SIDEPARAM0");

            entity.Property(e => e.Sideparam1)
                .HasColumnName("SIDEPARAM1");

            entity.Property(e => e.Sideparam2)
                .HasColumnName("SIDEPARAM2");

            entity.Property(e => e.Sideparam3)
                .HasColumnName("SIDEPARAM3");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<GstapplActionLog>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("gstappl_action_log");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Action)
                .HasColumnName("ACTION");

            entity.Property(e => e.ApplId)
                .HasColumnName("APPL_ID");

            entity.Property(e => e.Comment)
                .HasColumnName("COMMENT");

            entity.Property(e => e.RoleId)
                .HasColumnName("ROLE_ID");

            entity.Property(e => e.StageId)
                .HasColumnName("STAGE_ID");

            entity.Property(e => e.Time)
                .HasColumnName("TIME");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");
        });

        modelBuilder.Entity<GstapplCar>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("gstappl_car");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.ApplId)
                .HasColumnName("APPL_ID");

            entity.Property(e => e.Color)
                .HasColumnName("COLOR");

            entity.Property(e => e.Comment)
                .HasColumnName("COMMENT");

            entity.Property(e => e.Lpnumber)
                .HasColumnName("LPNUMBER");

            entity.Property(e => e.Model)
                .HasColumnName("MODEL");
        });

        modelBuilder.Entity<GstapplEmprolebindings>(entity =>
        {
            entity.HasKey(e => new { e.EmpId, e.RoleId });

            entity.ToTable("gstappl_emprolebindings");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.RoleId)
                .HasColumnName("ROLE_ID");
        });

        modelBuilder.Entity<GstapplPeople>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("gstappl_people");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.ApplId)
                .HasColumnName("APPL_ID");

            entity.Property(e => e.Birthdate)
                .HasColumnName("BIRTHDATE");

            entity.Property(e => e.Comment)
                .HasColumnName("COMMENT");

            entity.Property(e => e.Docname)
                .HasColumnName("DOCNAME");

            entity.Property(e => e.Docnumber)
                .HasColumnName("DOCNUMBER");

            entity.Property(e => e.Doctype)
                .HasColumnName("DOCTYPE");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<GstapplRoles>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("gstappl_roles");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.CanEditRules)
                .HasColumnName("CAN_EDIT_RULES");

            entity.Property(e => e.CanMoveBack)
                .HasColumnName("CAN_MOVE_BACK");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<GstapplRoutes>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("gstappl_routes");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<GstapplRoutesStages>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("gstappl_routes_stages");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.NotifyCreator)
                .HasColumnName("NOTIFY_CREATOR");

            entity.Property(e => e.NotifyRoles)
                .HasColumnName("NOTIFY_ROLES");

            entity.Property(e => e.OrderIdx)
                .HasColumnName("ORDER_IDX");

            entity.Property(e => e.RouteId)
                .HasColumnName("ROUTE_ID");
        });

        modelBuilder.Entity<GstapplRulebindings>(entity =>
        {
            entity.HasKey(e => new { e.AppId, e.RuleId });

            entity.ToTable("gstappl_rulebindings");

            entity.Property(e => e.AppId)
                .HasColumnName("APP_ID");

            entity.Property(e => e.RuleId)
                .HasColumnName("RULE_ID");
        });

        modelBuilder.Entity<GstapplStagerolebindings>(entity =>
        {
            entity.HasKey(e => new { e.StageId, e.RoleId });

            entity.ToTable("gstappl_stagerolebindings");

            entity.Property(e => e.StageId)
                .HasColumnName("STAGE_ID");

            entity.Property(e => e.RoleId)
                .HasColumnName("ROLE_ID");
        });

        modelBuilder.Entity<Guestbindings>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("guestbindings");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Address)
                .HasColumnName("ADDRESS");

            entity.Property(e => e.ApplId)
                .HasColumnName("APPL_ID");

            entity.Property(e => e.Birthdate)
                .HasColumnName("BIRTHDATE");

            entity.Property(e => e.CardId)
                .HasColumnName("CARD_ID");

            entity.Property(e => e.Cometo)
                .HasColumnName("COMETO");

            entity.Property(e => e.Comment)
                .HasColumnName("COMMENT");

            entity.Property(e => e.Datecre)
                .HasColumnName("DATECRE");

            entity.Property(e => e.Dateexp)
                .HasColumnName("DATEEXP");

            entity.Property(e => e.Datefin)
                .HasColumnName("DATEFIN");

            entity.Property(e => e.Docname)
                .HasColumnName("DOCNAME");

            entity.Property(e => e.Doctype)
                .HasColumnName("DOCTYPE");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.Lprnumber)
                .HasColumnName("LPRNUMBER");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Number)
                .HasColumnName("NUMBER");

            entity.Property(e => e.Series)
                .HasColumnName("SERIES");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.Whenissued)
                .HasColumnName("WHENISSUED");

            entity.Property(e => e.Whoissued)
                .HasColumnName("WHOISSUED");
        });

        modelBuilder.Entity<Guestphoto>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("guestphoto");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.HiresRaster)
                .HasColumnName("HIRES_RASTER");

            entity.Property(e => e.PreviewRaster)
                .HasColumnName("PREVIEW_RASTER");

            entity.Property(e => e.Ts)
                .HasColumnName("TS");

            entity.Property(e => e.TvaDesc)
                .HasColumnName("TVA_DESC");

            entity.Property(e => e.TvaDescts)
                .HasColumnName("TVA_DESCTS");

            entity.Property(e => e.TvaDesctype)
                .HasColumnName("TVA_DESCTYPE");
        });

        modelBuilder.Entity<Guestrulebindings>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("guestrulebindings");

            entity.Property(e => e.BindingId)
                .HasColumnName("BINDING_ID");

            entity.Property(e => e.RuleId)
                .HasColumnName("RULE_ID");
        });

        modelBuilder.Entity<Ind>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("ind");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<IndActions>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("ind_actions");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.LedC1)
                .HasColumnName("LED_C1");

            entity.Property(e => e.LedC2)
                .HasColumnName("LED_C2");

            entity.Property(e => e.LedL1)
                .HasColumnName("LED_L1");

            entity.Property(e => e.LedL2)
                .HasColumnName("LED_L2");

            entity.Property(e => e.LedRepCount)
                .HasColumnName("LED_REP_COUNT");

            entity.Property(e => e.LedType)
                .HasColumnName("LED_TYPE");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Priority)
                .HasColumnName("PRIORITY");

            entity.Property(e => e.SndDigL1)
                .HasColumnName("SND_DIG_L1");

            entity.Property(e => e.SndDigL2)
                .HasColumnName("SND_DIG_L2");

            entity.Property(e => e.SndDigRepCount)
                .HasColumnName("SND_DIG_REP_COUNT");

            entity.Property(e => e.SndType)
                .HasColumnName("SND_TYPE");

            entity.Property(e => e.SndWavId)
                .HasColumnName("SND_WAV_ID");

            entity.Property(e => e.SndWavRepCount)
                .HasColumnName("SND_WAV_REP_COUNT");

            entity.Property(e => e.SndWavRepDelta)
                .HasColumnName("SND_WAV_REP_DELTA");
        });

        modelBuilder.Entity<IndEvtActBinding>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.ProfileId, e.EventCode });

            entity.ToTable("ind_evt_act_binding");

            entity.Property(e => e.ProfileId)
                .HasColumnName("PROFILE_ID");

            entity.Property(e => e.EventCode)
                .HasColumnName("EVENT_CODE");

            entity.Property(e => e.ActionId)
                .HasColumnName("ACTION_ID");
        });

        modelBuilder.Entity<IndSounds>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("ind_sounds");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Data)
                .HasColumnName("DATA");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Ipcammodels>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("ipcammodels");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<IpcammodelsChannels>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("ipcammodels_channels");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.IpcammodelId)
                .HasColumnName("IPCAMMODEL_ID");

            entity.Property(e => e.Url)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<IsbcId>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("isbc_id");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.ActivationCode)
                .HasColumnName("ACTIVATION_CODE");

            entity.Property(e => e.IssuedEmpId)
                .HasColumnName("ISSUED_EMP_ID");

            entity.Property(e => e.IssuedTime)
                .HasColumnName("ISSUED_TIME");

            entity.Property(e => e.Status)
                .HasColumnName("STATUS");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");
        });

        modelBuilder.Entity<KrDevices>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("kr_devices");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.CamParam1)
                .HasColumnName("CAM_PARAM1");

            entity.Property(e => e.CamServerId)
                .HasColumnName("CAM_SERVER_ID");

            entity.Property(e => e.KgAddr)
                .HasColumnName("KG_ADDR");

            entity.Property(e => e.KgIp)
                .HasColumnName("KG_IP");

            entity.Property(e => e.KgReaderType)
                .HasColumnName("KG_READER_TYPE");

            entity.Property(e => e.KgRoomNumber)
                .HasColumnName("KG_ROOM_NUMBER");

            entity.Property(e => e.KgSerialNumber)
                .HasColumnName("KG_SERIAL_NUMBER");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Vendor)
                .HasColumnName("VENDOR");
        });

        modelBuilder.Entity<KrKeys>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("kr_keys");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Alarmlines)
                .HasColumnName("ALARMLINES");

            entity.Property(e => e.KeyrackId)
                .HasColumnName("KEYRACK_ID");

            entity.Property(e => e.KgAddr)
                .HasColumnName("KG_ADDR");

            entity.Property(e => e.KgBlockNumber)
                .HasColumnName("KG_BLOCK_NUMBER");

            entity.Property(e => e.KgCellNumber)
                .HasColumnName("KG_CELL_NUMBER");

            entity.Property(e => e.KgDelayHour)
                .HasColumnName("KG_DELAY_HOUR");

            entity.Property(e => e.KgDelayMin)
                .HasColumnName("KG_DELAY_MIN");

            entity.Property(e => e.KgExtensionParentId)
                .HasColumnName("KG_EXTENSION_PARENT_ID");

            entity.Property(e => e.KgKeyIbutton)
                .HasColumnName("KG_KEY_IBUTTON");

            entity.Property(e => e.KgKeyListExtension)
                .HasColumnName("KG_KEY_LIST_EXTENSION");

            entity.Property(e => e.KgKeyNeedConfirm)
                .HasColumnName("KG_KEY_NEED_CONFIRM");

            entity.Property(e => e.KgKeylistTextIndex)
                .HasColumnName("KG_KEYLIST_TEXT_INDEX");

            entity.Property(e => e.KgParents)
                .HasColumnName("KG_PARENTS");

            entity.Property(e => e.KgReturnDelay)
                .HasColumnName("KG_RETURN_DELAY");

            entity.Property(e => e.KgReturnDelayType)
                .HasColumnName("KG_RETURN_DELAY_TYPE");

            entity.Property(e => e.KgReturnTimeHour)
                .HasColumnName("KG_RETURN_TIME_HOUR");

            entity.Property(e => e.KgReturnTimeMin)
                .HasColumnName("KG_RETURN_TIME_MIN");

            entity.Property(e => e.KgRoomNumber)
                .HasColumnName("KG_ROOM_NUMBER");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<KrKgHolidays>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("kr_kg_holidays");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Addr)
                .HasColumnName("ADDR");

            entity.Property(e => e.Day)
                .HasColumnName("DAY");

            entity.Property(e => e.KeyrackId)
                .HasColumnName("KEYRACK_ID");

            entity.Property(e => e.Month)
                .HasColumnName("MONTH");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.ParentId)
                .HasColumnName("PARENT_ID");
        });

        modelBuilder.Entity<KrKgKeyAccounts>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("kr_kg_key_accounts");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Addr)
                .HasColumnName("ADDR");

            entity.Property(e => e.KeyAuthId)
                .HasColumnName("KEY_AUTH_ID");

            entity.Property(e => e.KeyrackId)
                .HasColumnName("KEYRACK_ID");

            entity.Property(e => e.NameTextId)
                .HasColumnName("NAME_TEXT_ID");

            entity.Property(e => e.Password)
                .HasColumnName("PASSWORD");

            entity.Property(e => e.PersonalId)
                .HasColumnName("PERSONAL_ID");

            entity.Property(e => e.PhoneTextId)
                .HasColumnName("PHONE_TEXT_ID");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");
        });

        modelBuilder.Entity<KrKgKeyAuth>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("kr_kg_key_auth");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Addr)
                .HasColumnName("ADDR");

            entity.Property(e => e.ConfirmFr)
                .HasColumnName("CONFIRM_FR");

            entity.Property(e => e.ConfirmOpr)
                .HasColumnName("CONFIRM_OPR");

            entity.Property(e => e.KeyrackId)
                .HasColumnName("KEYRACK_ID");

            entity.Property(e => e.KeysId)
                .HasColumnName("KEYS_ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.RuleId)
                .HasColumnName("RULE_ID");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.TzId)
                .HasColumnName("TZ_ID");
        });

        modelBuilder.Entity<KrKgTexts>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("kr_kg_texts");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Addr)
                .HasColumnName("ADDR");

            entity.Property(e => e.KeyrackId)
                .HasColumnName("KEYRACK_ID");

            entity.Property(e => e.Text)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<KrKgTz>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("kr_kg_tz");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Addr)
                .HasColumnName("ADDR");

            entity.Property(e => e.IntervalEndHour)
                .HasColumnName("INTERVAL_END_HOUR");

            entity.Property(e => e.IntervalEndMin)
                .HasColumnName("INTERVAL_END_MIN");

            entity.Property(e => e.IntervalStartHour)
                .HasColumnName("INTERVAL_START_HOUR");

            entity.Property(e => e.IntervalStartMin)
                .HasColumnName("INTERVAL_START_MIN");

            entity.Property(e => e.IntervalType)
                .HasColumnName("INTERVAL_TYPE");

            entity.Property(e => e.KeyrackId)
                .HasColumnName("KEYRACK_ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.ParentId)
                .HasColumnName("PARENT_ID");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<Logbuffer>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("logbuffer");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Ap)
                .HasColumnName("AP");

            entity.Property(e => e.BaseAddr)
                .HasColumnName("BASE_ADDR");

            entity.Property(e => e.Data)
                .HasColumnName("DATA");
        });

        modelBuilder.Entity<MifareKeyHistory>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("mifare_key_history");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.KeyString)
                .HasColumnName("KEY_STRING");

            entity.Property(e => e.KeyType)
                .HasColumnName("KEY_TYPE");

            entity.Property(e => e.Ts)
                .HasColumnName("TS");
        });

        modelBuilder.Entity<MiscMolockers>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("misc_molockers");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.ActualNumber)
                .HasColumnName("ACTUAL_NUMBER");

            entity.Property(e => e.Branch)
                .HasColumnName("BRANCH");

            entity.Property(e => e.Cellnum)
                .HasColumnName("CELLNUM");

            entity.Property(e => e.Celltype)
                .HasColumnName("CELLTYPE");

            entity.Property(e => e.CfpSn)
                .HasColumnName("CFP_SN");

            entity.Property(e => e.Groupnum)
                .HasColumnName("GROUPNUM");

            entity.Property(e => e.Ip)
                .HasColumnName("IP");

            entity.Property(e => e.OccupiedBy)
                .HasColumnName("OCCUPIED_BY");

            entity.Property(e => e.OccupiedTime)
                .HasColumnName("OCCUPIED_TIME");

            entity.Property(e => e.OccupiedUseCount)
                .HasColumnName("OCCUPIED_USE_COUNT");

            entity.Property(e => e.OrderIdx)
                .HasColumnName("ORDER_IDX");

            entity.Property(e => e.Port)
                .HasColumnName("PORT");

            entity.Property(e => e.Status)
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<MiscMolockersLog>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("misc_molockers_log");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Action)
                .HasColumnName("ACTION");

            entity.Property(e => e.Actiontime)
                .HasColumnName("ACTIONTIME");

            entity.Property(e => e.Cellid)
                .HasColumnName("CELLID");

            entity.Property(e => e.Deviceid)
                .HasColumnName("DEVICEID");

            entity.Property(e => e.Personalid)
                .HasColumnName("PERSONALID");
        });

        modelBuilder.Entity<Monapselection>(entity =>
        {
            //entity.HasNoKey();

            entity.HasKey(e => new { e.ApId, e.MonitemId, e.UserId });

            entity.ToTable("monapselection");

            entity.Property(e => e.ApId)
                .HasColumnName("AP_ID");

            entity.Property(e => e.MonitemId)
                .HasColumnName("MONITEM_ID");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");
        });

        modelBuilder.Entity<Monframes>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("monframes");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Height)
                .HasColumnName("HEIGHT");

            entity.Property(e => e.MainFrame)
                .HasColumnName("MAIN_FRAME");

            entity.Property(e => e.Monviewid)
                .HasColumnName("MONVIEWID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Width)
                .HasColumnName("WIDTH");
        });

        modelBuilder.Entity<Monitemalms>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("monitemalms");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.AlarmlineId)
                .HasColumnName("ALARMLINE_ID");

            entity.Property(e => e.MatrixtabId)
                .HasColumnName("MATRIXTAB_ID");
        });

        modelBuilder.Entity<Monitemalmtabs>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("monitemalmtabs");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.MonitemId)
                .HasColumnName("MONITEM_ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Monitemaps>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.MonitemId, e.ApId });

            entity.ToTable("monitemaps");

            entity.Property(e => e.MonitemId)
                .HasColumnName("MONITEM_ID");

            entity.Property(e => e.ApId)
                .HasColumnName("AP_ID");
        });

        modelBuilder.Entity<Monitememps>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.MonitemId, e.EmpId });

            entity.ToTable("monitememps");

            entity.Property(e => e.MonitemId)
                .HasColumnName("MONITEM_ID");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");
        });

        modelBuilder.Entity<Monitems>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("monitems");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.ActionType)
                .HasColumnName("ACTION_TYPE");

            entity.Property(e => e.AlarmMatrixWidth)
                .HasColumnName("ALARM_MATRIX_WIDTH");

            entity.Property(e => e.ApRestricted)
                .HasColumnName("AP_RESTRICTED");

            entity.Property(e => e.AssociatedEmp)
                .HasColumnName("ASSOCIATED_EMP");

            entity.Property(e => e.AutohideEnabled)
                .HasColumnName("AUTOHIDE_ENABLED");

            entity.Property(e => e.AutohideTimer)
                .HasColumnName("AUTOHIDE_TIMER");

            entity.Property(e => e.Bindata)
                .HasColumnName("BINDATA");

            entity.Property(e => e.BindataTs)
                .HasColumnName("BINDATA_TS");

            entity.Property(e => e.Cmd)
                .HasColumnName("CMD");

            entity.Property(e => e.CondSearchFor)
                .HasColumnName("COND_SEARCH_FOR");

            entity.Property(e => e.CondSearchWhere)
                .HasColumnName("COND_SEARCH_WHERE");

            entity.Property(e => e.CondType)
                .HasColumnName("COND_TYPE");

            entity.Property(e => e.Delay)
                .HasColumnName("DELAY");

            entity.Property(e => e.EvsourceFilter)
                .HasColumnName("EVSOURCE_FILTER");

            entity.Property(e => e.EvsourceItemId)
                .HasColumnName("EVSOURCE_ITEM_ID");

            entity.Property(e => e.EvsourceType)
                .HasColumnName("EVSOURCE_TYPE");

            entity.Property(e => e.Flags)
                .HasColumnName("FLAGS");

            entity.Property(e => e.FontSize)
                .HasColumnName("FONT_SIZE");

            entity.Property(e => e.Height)
                .HasColumnName("HEIGHT");

            entity.Property(e => e.ImagePersonalimgName)
                .HasColumnName("IMAGE_PERSONALIMG_NAME");

            entity.Property(e => e.ImageType)
                .HasColumnName("IMAGE_TYPE");

            entity.Property(e => e.Monframeid)
                .HasColumnName("MONFRAMEID");

            entity.Property(e => e.OprPanel)
                .HasColumnName("OPR_PANEL");

            entity.Property(e => e.Param1)
                .HasColumnName("PARAM1");

            entity.Property(e => e.Param2)
                .HasColumnName("PARAM2");

            entity.Property(e => e.ProcessHtml)
                .HasColumnName("PROCESS_HTML");

            entity.Property(e => e.RedWarningEvents)
                .HasColumnName("RED_WARNING_EVENTS");

            entity.Property(e => e.TpFormat)
                .HasColumnName("TP_FORMAT");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.VideoSource)
                .HasColumnName("VIDEO_SOURCE");

            entity.Property(e => e.VideochannelParam1)
                .HasColumnName("VIDEOCHANNEL_PARAM1");

            entity.Property(e => e.VideochannelParam2)
                .HasColumnName("VIDEOCHANNEL_PARAM2");

            entity.Property(e => e.VideochannelParam3)
                .HasColumnName("VIDEOCHANNEL_PARAM3");

            entity.Property(e => e.VideochannelServerId)
                .HasColumnName("VIDEOCHANNEL_SERVER_ID");

            entity.Property(e => e.Width)
                .HasColumnName("WIDTH");
        });

        modelBuilder.Entity<Monitemzones>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.MonitemId, e.ZoneId });

            entity.ToTable("monitemzones");

            entity.Property(e => e.MonitemId)
                .HasColumnName("MONITEM_ID");

            entity.Property(e => e.ZoneId)
                .HasColumnName("ZONE_ID");
        });

        modelBuilder.Entity<Monusers>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.MonviewId, e.UserId });

            entity.ToTable("monusers");

            entity.Property(e => e.MonviewId)
                .HasColumnName("MONVIEW_ID");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");
        });

        modelBuilder.Entity<Monviews>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("monviews");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Oditems>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("oditems");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.BeginTime)
                .HasColumnName("BEGIN_TIME");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.EndTime)
                .HasColumnName("END_TIME");

            entity.Property(e => e.Extid)
                .HasColumnName("EXTID");

            entity.Property(e => e.Extsourceid)
                .HasColumnName("EXTSOURCEID");

            entity.Property(e => e.Number)
                .HasColumnName("NUMBER");

            entity.Property(e => e.TypeId)
                .HasColumnName("TYPE_ID");
        });

        modelBuilder.Entity<Odtypes>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("odtypes");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.CalculationType)
                .HasColumnName("CALCULATION_TYPE");

            entity.Property(e => e.Code)
                .HasColumnName("CODE");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Priority)
                .HasColumnName("PRIORITY");
        });

        modelBuilder.Entity<Onec8servers>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("onec8servers");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Componenttype)
                .HasColumnName("COMPONENTTYPE");

            entity.Property(e => e.ConnectionString)
                .HasColumnName("CONNECTION_STRING");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Platformver)
                .HasColumnName("PLATFORMVER");

            entity.Property(e => e.Zupver)
                .HasColumnName("ZUPVER");
        });

        modelBuilder.Entity<OsdpDevices>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("osdp_devices");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Addr)
                .HasColumnName("ADDR");

            entity.Property(e => e.Baudrate)
                .HasColumnName("BAUDRATE");

            entity.Property(e => e.Checksum)
                .HasColumnName("CHECKSUM");

            entity.Property(e => e.DevId)
                .HasColumnName("DEV_ID");

            entity.Property(e => e.EncprofileId)
                .HasColumnName("ENCPROFILE_ID");

            entity.Property(e => e.IndprofileId)
                .HasColumnName("INDPROFILE_ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Number)
                .HasColumnName("NUMBER");
        });

        modelBuilder.Entity<OsdpEncprofile>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("osdp_encprofile");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.AesEnabled)
                .HasColumnName("AES_ENABLED");

            entity.Property(e => e.AesKey)
                .HasColumnName("AES_KEY");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Paramb>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Name);
            entity.Property(e => e.Name).ValueGeneratedNever();

            entity.ToTable("paramb");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Paramvalue)
                .HasColumnName("PARAMVALUE");
        });

        modelBuilder.Entity<Parami>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Name);
            entity.Property(e => e.Name).ValueGeneratedNever();

            entity.ToTable("parami");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Paramvalue)
                .HasColumnName("PARAMVALUE");
        });

        modelBuilder.Entity<ParkingPassCosts>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("parking_pass_costs");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Cost)
                .HasColumnName("COST");

            entity.Property(e => e.PassId)
                .HasColumnName("PASS_ID");

            entity.Property(e => e.TariffPartId)
                .HasColumnName("TARIFF_PART_ID");
        });

        modelBuilder.Entity<ParkingPasses>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("parking_passes");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.EntryDevId)
                .HasColumnName("ENTRY_DEV_ID");

            entity.Property(e => e.EntryTime)
                .HasColumnName("ENTRY_TIME");

            entity.Property(e => e.ExitTime)
                .HasColumnName("EXIT_TIME");

            entity.Property(e => e.TariffId)
                .HasColumnName("TARIFF_ID");
        });

        modelBuilder.Entity<ParkingTariffPartDaytimes>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("parking_tariff_part_daytimes");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.BeginTime)
                .HasColumnName("BEGIN_TIME");

            entity.Property(e => e.DayNumber)
                .HasColumnName("DAY_NUMBER");

            entity.Property(e => e.EndTime)
                .HasColumnName("END_TIME");

            entity.Property(e => e.TariffPartId)
                .HasColumnName("TARIFF_PART_ID");
        });

        modelBuilder.Entity<ParkingTariffParts>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("parking_tariff_parts");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.TariffId)
                .HasColumnName("TARIFF_ID");
        });

        modelBuilder.Entity<ParkingTariffs>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("parking_tariffs");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Patrol>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("patrol");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.Enddate)
                .HasColumnName("ENDDATE");

            entity.Property(e => e.MaxInterval)
                .HasColumnName("MAX_INTERVAL");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Nrules)
                .HasColumnName("NRULES");

            entity.Property(e => e.Rule0)
                .HasColumnName("RULE0");

            entity.Property(e => e.Rule1)
                .HasColumnName("RULE1");

            entity.Property(e => e.Rule10)
                .HasColumnName("RULE10");

            entity.Property(e => e.Rule11)
                .HasColumnName("RULE11");

            entity.Property(e => e.Rule12)
                .HasColumnName("RULE12");

            entity.Property(e => e.Rule13)
                .HasColumnName("RULE13");

            entity.Property(e => e.Rule14)
                .HasColumnName("RULE14");

            entity.Property(e => e.Rule15)
                .HasColumnName("RULE15");

            entity.Property(e => e.Rule16)
                .HasColumnName("RULE16");

            entity.Property(e => e.Rule17)
                .HasColumnName("RULE17");

            entity.Property(e => e.Rule18)
                .HasColumnName("RULE18");

            entity.Property(e => e.Rule19)
                .HasColumnName("RULE19");

            entity.Property(e => e.Rule2)
                .HasColumnName("RULE2");

            entity.Property(e => e.Rule20)
                .HasColumnName("RULE20");

            entity.Property(e => e.Rule21)
                .HasColumnName("RULE21");

            entity.Property(e => e.Rule22)
                .HasColumnName("RULE22");

            entity.Property(e => e.Rule23)
                .HasColumnName("RULE23");

            entity.Property(e => e.Rule24)
                .HasColumnName("RULE24");

            entity.Property(e => e.Rule25)
                .HasColumnName("RULE25");

            entity.Property(e => e.Rule26)
                .HasColumnName("RULE26");

            entity.Property(e => e.Rule27)
                .HasColumnName("RULE27");

            entity.Property(e => e.Rule28)
                .HasColumnName("RULE28");

            entity.Property(e => e.Rule29)
                .HasColumnName("RULE29");

            entity.Property(e => e.Rule3)
                .HasColumnName("RULE3");

            entity.Property(e => e.Rule30)
                .HasColumnName("RULE30");

            entity.Property(e => e.Rule31)
                .HasColumnName("RULE31");

            entity.Property(e => e.Rule4)
                .HasColumnName("RULE4");

            entity.Property(e => e.Rule5)
                .HasColumnName("RULE5");

            entity.Property(e => e.Rule6)
                .HasColumnName("RULE6");

            entity.Property(e => e.Rule7)
                .HasColumnName("RULE7");

            entity.Property(e => e.Rule8)
                .HasColumnName("RULE8");

            entity.Property(e => e.Rule9)
                .HasColumnName("RULE9");

            entity.Property(e => e.Startdate)
                .HasColumnName("STARTDATE");
        });

        modelBuilder.Entity<PatrolDevbindings>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.DevId, e.RuleId });

            entity.ToTable("patrol_devbindings");

            entity.Property(e => e.DevId)
                .HasColumnName("DEV_ID");

            entity.Property(e => e.RuleId)
                .HasColumnName("RULE_ID");
        });

        modelBuilder.Entity<PatrolEmpbindings>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.EmpId, e.RuleId });

            entity.ToTable("patrol_empbindings");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.RuleId)
                .HasColumnName("RULE_ID");
        });

        modelBuilder.Entity<PatrolRt>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.RuleId, e.ApId });

            entity.ToTable("patrol_rt");

            entity.Property(e => e.RuleId)
                .HasColumnName("RULE_ID");

            entity.Property(e => e.ApId)
                .HasColumnName("AP_ID");

            entity.Property(e => e.LastLogEmp)
                .HasColumnName("LAST_LOG_EMP");

            entity.Property(e => e.LastLogTime)
                .HasColumnName("LAST_LOG_TIME");

            entity.Property(e => e.LastViolationTime)
                .HasColumnName("LAST_VIOLATION_TIME");
        });

        modelBuilder.Entity<Payaccs>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("payaccs");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Curvalue)
                .HasColumnName("CURVALUE");

            entity.Property(e => e.Objid)
                .HasColumnName("OBJID");

            entity.Property(e => e.Typeid)
                .HasColumnName("TYPEID");
        });

        modelBuilder.Entity<Payacctypes>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("payacctypes");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.CreateByDefault)
                .HasColumnName("CREATE_BY_DEFAULT");

            entity.Property(e => e.Credit)
                .HasColumnName("CREDIT");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Unitname)
                .HasColumnName("UNITNAME");

            entity.Property(e => e.Valuestep)
                .HasColumnName("VALUESTEP");
        });

        modelBuilder.Entity<Paylog>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("paylog");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Accid)
                .HasColumnName("ACCID");

            entity.Property(e => e.Apid)
                .HasColumnName("APID");

            entity.Property(e => e.Logtime)
                .HasColumnName("LOGTIME");

            entity.Property(e => e.Newvalue)
                .HasColumnName("NEWVALUE");

            entity.Property(e => e.Oldvalue)
                .HasColumnName("OLDVALUE");

            entity.Property(e => e.Optime)
                .HasColumnName("OPTIME");

            entity.Property(e => e.Optype)
                .HasColumnName("OPTYPE");

            entity.Property(e => e.Userid)
                .HasColumnName("USERID");
        });

        modelBuilder.Entity<Paylogdetails>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("paylogdetails");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Costvalue)
                .HasColumnName("COSTVALUE");

            entity.Property(e => e.Countvalue)
                .HasColumnName("COUNTVALUE");

            entity.Property(e => e.Itemid)
                .HasColumnName("ITEMID");

            entity.Property(e => e.Logid)
                .HasColumnName("LOGID");
        });

        modelBuilder.Entity<Paymenuitemacctypes>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Itemid, e.Typeid });

            entity.ToTable("paymenuitemacctypes");

            entity.Property(e => e.Itemid)
                .HasColumnName("ITEMID");

            entity.Property(e => e.Typeid)
                .HasColumnName("TYPEID");
        });

        modelBuilder.Entity<Paymenuitems>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("paymenuitems");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Color)
                .HasColumnName("COLOR");

            entity.Property(e => e.CostParam1)
                .HasColumnName("COST_PARAM1");

            entity.Property(e => e.CostType)
                .HasColumnName("COST_TYPE");

            entity.Property(e => e.Extid)
                .HasColumnName("EXTID");

            entity.Property(e => e.IncaccTypeid)
                .HasColumnName("INCACC_TYPEID");

            entity.Property(e => e.IncaccValue)
                .HasColumnName("INCACC_VALUE");

            entity.Property(e => e.Menuid)
                .HasColumnName("MENUID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Number)
                .HasColumnName("NUMBER");

            entity.Property(e => e.RestrCountParam1)
                .HasColumnName("RESTR_COUNT_PARAM1");

            entity.Property(e => e.RestrCountType)
                .HasColumnName("RESTR_COUNT_TYPE");

            entity.Property(e => e.RestrDay1)
                .HasColumnName("RESTR_DAY1");

            entity.Property(e => e.RestrDay2)
                .HasColumnName("RESTR_DAY2");

            entity.Property(e => e.RestrDay3)
                .HasColumnName("RESTR_DAY3");

            entity.Property(e => e.RestrDay4)
                .HasColumnName("RESTR_DAY4");

            entity.Property(e => e.RestrDay5)
                .HasColumnName("RESTR_DAY5");

            entity.Property(e => e.RestrDay6)
                .HasColumnName("RESTR_DAY6");

            entity.Property(e => e.RestrDay7)
                .HasColumnName("RESTR_DAY7");

            entity.Property(e => e.RestrTime1)
                .HasColumnName("RESTR_TIME1");

            entity.Property(e => e.RestrTime2)
                .HasColumnName("RESTR_TIME2");

            entity.Property(e => e.Shortname)
                .HasColumnName("SHORTNAME");

            entity.Property(e => e.Unitname)
                .HasColumnName("UNITNAME");
        });

        modelBuilder.Entity<Paymenuitemsdates>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("paymenuitemsdates");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.AllowedDate)
                .HasColumnName("ALLOWED_DATE");

            entity.Property(e => e.PaymenuitemId)
                .HasColumnName("PAYMENUITEM_ID");
        });

        modelBuilder.Entity<Paymenus>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("paymenus");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Color)
                .HasColumnName("COLOR");

            entity.Property(e => e.Enddate)
                .HasColumnName("ENDDATE");

            entity.Property(e => e.Extid)
                .HasColumnName("EXTID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Startdate)
                .HasColumnName("STARTDATE");
        });

        modelBuilder.Entity<Paymenuusers>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Menuid, e.Userid });

            entity.ToTable("paymenuusers");

            entity.Property(e => e.Menuid)
                .HasColumnName("MENUID");

            entity.Property(e => e.Userid)
                .HasColumnName("USERID");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("personal");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.AdDomainId)
                .HasColumnName("AD_DOMAIN_ID");

            entity.Property(e => e.AdEnabled)
                .HasColumnName("AD_ENABLED");

            entity.Property(e => e.AdSyncPending)
                .HasColumnName("AD_SYNC_PENDING");

            entity.Property(e => e.AdUserDn)
                .HasColumnName("AD_USER_DN");

            entity.Property(e => e.ApbOn)
                .HasColumnName("APB_ON");

            entity.Property(e => e.AplExptime)
                .HasColumnName("APL_EXPTIME");

            entity.Property(e => e.AplOn)
                .HasColumnName("APL_ON");

            entity.Property(e => e.Badge)
                .HasColumnName("BADGE");

            entity.Property(e => e.Badgeb)
                .HasColumnName("BADGEB");

            entity.Property(e => e.Boolparam1)
                .HasColumnName("BOOLPARAM1");

            entity.Property(e => e.Boolparam2)
                .HasColumnName("BOOLPARAM2");

            entity.Property(e => e.Boolparam3)
                .HasColumnName("BOOLPARAM3");

            entity.Property(e => e.Boolparam4)
                .HasColumnName("BOOLPARAM4");

            entity.Property(e => e.Codekey)
                .HasColumnName("CODEKEY");

            entity.Property(e => e.CodekeyDispFormat)
                .HasColumnName("CODEKEY_DISP_FORMAT");

            entity.Property(e => e.Codekeytime)
                .HasColumnName("CODEKEYTIME");

            entity.Property(e => e.Createdtime)
                .HasColumnName("CREATEDTIME");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.Email)
                .HasColumnName("EMAIL");

            entity.Property(e => e.EmailSubject)
                .HasColumnName("EMAIL_SUBJECT");

            entity.Property(e => e.Exptime)
                .HasColumnName("EXPTIME");

            entity.Property(e => e.Extid)
                .HasColumnName("EXTID");

            entity.Property(e => e.Extsourceid)
                .HasColumnName("EXTSOURCEID");

            entity.Property(e => e.Firedtime)
                .HasColumnName("FIREDTIME");

            entity.Property(e => e.IdleLastntfy)
                .HasColumnName("IDLE_LASTNTFY");

            entity.Property(e => e.LastlprAp)
                .HasColumnName("LASTLPR_AP");

            entity.Property(e => e.LastlprDir)
                .HasColumnName("LASTLPR_DIR");

            entity.Property(e => e.LastlprTime)
                .HasColumnName("LASTLPR_TIME");

            entity.Property(e => e.LastpassAp)
                .HasColumnName("LASTPASS_AP");

            entity.Property(e => e.Locationact)
                .HasColumnName("LOCATIONACT");

            entity.Property(e => e.Locationzone)
                .HasColumnName("LOCATIONZONE");

            entity.Property(e => e.MfUid)
                .HasColumnName("MF_UID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.NtfyEnddate)
                .HasColumnName("NTFY_ENDDATE");

            entity.Property(e => e.NtfyGstapplEnabled)
                .HasColumnName("NTFY_GSTAPPL_ENABLED");

            entity.Property(e => e.NtfyGstapplText)
                .HasColumnName("NTFY_GSTAPPL_TEXT");

            entity.Property(e => e.NtfyLastAp)
                .HasColumnName("NTFY_LAST_AP");

            entity.Property(e => e.NtfyLastDir)
                .HasColumnName("NTFY_LAST_DIR");

            entity.Property(e => e.NtfyLastSent)
                .HasColumnName("NTFY_LAST_SENT");

            entity.Property(e => e.NtfyLastTime)
                .HasColumnName("NTFY_LAST_TIME");

            entity.Property(e => e.NtfyPassEnabled)
                .HasColumnName("NTFY_PASS_ENABLED");

            entity.Property(e => e.NtfyPassText)
                .HasColumnName("NTFY_PASS_TEXT");

            entity.Property(e => e.NtfyPayDecEnabled)
                .HasColumnName("NTFY_PAY_DEC_ENABLED");

            entity.Property(e => e.NtfyPayDecText)
                .HasColumnName("NTFY_PAY_DEC_TEXT");

            entity.Property(e => e.NtfyPayDecThr)
                .HasColumnName("NTFY_PAY_DEC_THR");

            entity.Property(e => e.NtfyPayEnabled)
                .HasColumnName("NTFY_PAY_ENABLED");

            entity.Property(e => e.NtfyPayText)
                .HasColumnName("NTFY_PAY_TEXT");

            entity.Property(e => e.NtfyStartdate)
                .HasColumnName("NTFY_STARTDATE");

            entity.Property(e => e.NtfyType)
                .HasColumnName("NTFY_TYPE");

            entity.Property(e => e.ParentId)
                .HasColumnName("PARENT_ID");

            entity.Property(e => e.Pos)
                .HasColumnName("POS");

            entity.Property(e => e.Sideparam0)
                .HasColumnName("SIDEPARAM0");

            entity.Property(e => e.Sideparam1)
                .HasColumnName("SIDEPARAM1");

            entity.Property(e => e.Sideparam2)
                .HasColumnName("SIDEPARAM2");

            entity.Property(e => e.Sideparam3)
                .HasColumnName("SIDEPARAM3");

            entity.Property(e => e.Sideparam4)
                .HasColumnName("SIDEPARAM4");

            entity.Property(e => e.Sideparam5)
                .HasColumnName("SIDEPARAM5");

            entity.Property(e => e.SmsTargetnumber)
                .HasColumnName("SMS_TARGETNUMBER");

            entity.Property(e => e.SoaaKeyStatus)
                .HasColumnName("SOAA_KEY_STATUS");

            entity.Property(e => e.SoaaUid)
                .HasColumnName("SOAA_UID");

            entity.Property(e => e.Status)
                .HasColumnName("STATUS");

            entity.Property(e => e.Tabid)
                .HasColumnName("TABID");

            entity.Property(e => e.TelegramChatid)
                .HasColumnName("TELEGRAM_CHATID");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.UserAlmrestriction)
                .HasColumnName("USER_ALMRESTRICTION");

            entity.Property(e => e.UserApplsEditAll)
                .HasColumnName("USER_APPLS_EDIT_ALL");

            entity.Property(e => e.UserApplsEditCurrent)
                .HasColumnName("USER_APPLS_EDIT_CURRENT");

            entity.Property(e => e.UserApplsSeeAll)
                .HasColumnName("USER_APPLS_SEE_ALL");

            entity.Property(e => e.UserApplsSeeCurrent)
                .HasColumnName("USER_APPLS_SEE_CURRENT");

            entity.Property(e => e.UserAprestriction)
                .HasColumnName("USER_APRESTRICTION");

            entity.Property(e => e.UserChownpass)
                .HasColumnName("USER_CHOWNPASS");

            entity.Property(e => e.UserCntlmodules)
                .HasColumnName("USER_CNTLMODULES");

            entity.Property(e => e.UserDepsrestriction)
                .HasColumnName("USER_DEPSRESTRICTION");

            entity.Property(e => e.UserEnabled)
                .HasColumnName("USER_ENABLED");

            entity.Property(e => e.UserExitpassword)
                .HasColumnName("USER_EXITPASSWORD");

            entity.Property(e => e.UserExitpasswordUsed)
                .HasColumnName("USER_EXITPASSWORD_USED");

            entity.Property(e => e.UserFloorsrestriction)
                .HasColumnName("USER_FLOORSRESTRICTION");

            entity.Property(e => e.UserGstapplCreate)
                .HasColumnName("USER_GSTAPPL_CREATE");

            entity.Property(e => e.UserGstapplModify)
                .HasColumnName("USER_GSTAPPL_MODIFY");

            entity.Property(e => e.UserGstapplRange)
                .HasColumnName("USER_GSTAPPL_RANGE");

            entity.Property(e => e.UserLocalsettings)
                .HasColumnName("USER_LOCALSETTINGS");

            entity.Property(e => e.UserLogin)
                .HasColumnName("USER_LOGIN");

            entity.Property(e => e.UserMonviewPolicy)
                .HasColumnName("USER_MONVIEW_POLICY");

            entity.Property(e => e.UserOif)
                .HasColumnName("USER_OIF");

            entity.Property(e => e.UserPassword)
                .HasColumnName("USER_PASSWORD");

            entity.Property(e => e.UserReprestriction)
                .HasColumnName("USER_REPRESTRICTION");

            entity.Property(e => e.UserTAlarm)
                .HasColumnName("USER_T_ALARM");

            entity.Property(e => e.UserTAlarmCmd)
                .HasColumnName("USER_T_ALARM_CMD");

            entity.Property(e => e.UserTAlarmEditconf)
                .HasColumnName("USER_T_ALARM_EDITCONF");

            entity.Property(e => e.UserTArchive)
                .HasColumnName("USER_T_ARCHIVE");

            entity.Property(e => e.UserTAutopark)
                .HasColumnName("USER_T_AUTOPARK");

            entity.Property(e => e.UserTCardlogin)
                .HasColumnName("USER_T_CARDLOGIN");

            entity.Property(e => e.UserTEditplans)
                .HasColumnName("USER_T_EDITPLANS");

            entity.Property(e => e.UserTEvents)
                .HasColumnName("USER_T_EVENTS");

            entity.Property(e => e.UserTFace)
                .HasColumnName("USER_T_FACE");

            entity.Property(e => e.UserTGuests)
                .HasColumnName("USER_T_GUESTS");

            entity.Property(e => e.UserTGuestsDeletepd)
                .HasColumnName("USER_T_GUESTS_DELETEPD");

            entity.Property(e => e.UserTGuestsEdit)
                .HasColumnName("USER_T_GUESTS_EDIT");

            entity.Property(e => e.UserTHw)
                .HasColumnName("USER_T_HW");

            entity.Property(e => e.UserTKeyguard)
                .HasColumnName("USER_T_KEYGUARD");

            entity.Property(e => e.UserTMolockers)
                .HasColumnName("USER_T_MOLOCKERS");

            entity.Property(e => e.UserTMon)
                .HasColumnName("USER_T_MON");

            entity.Property(e => e.UserTMonAllowanonpass)
                .HasColumnName("USER_T_MON_ALLOWANONPASS");

            entity.Property(e => e.UserTMonAllowauthpass)
                .HasColumnName("USER_T_MON_ALLOWAUTHPASS");

            entity.Property(e => e.UserTMonAllowpass)
                .HasColumnName("USER_T_MON_ALLOWPASS");

            entity.Property(e => e.UserTMonCntlalm)
                .HasColumnName("USER_T_MON_CNTLALM");

            entity.Property(e => e.UserTMonCntlap)
                .HasColumnName("USER_T_MON_CNTLAP");

            entity.Property(e => e.UserTNfcterminal)
                .HasColumnName("USER_T_NFCTERMINAL");

            entity.Property(e => e.UserTNfcterminalRegAuto)
                .HasColumnName("USER_T_NFCTERMINAL_REG_AUTO");

            entity.Property(e => e.UserTNfcterminalRegIn)
                .HasColumnName("USER_T_NFCTERMINAL_REG_IN");

            entity.Property(e => e.UserTNfcterminalRegOut)
                .HasColumnName("USER_T_NFCTERMINAL_REG_OUT");

            entity.Property(e => e.UserTOd)
                .HasColumnName("USER_T_OD");

            entity.Property(e => e.UserTParkingpay)
                .HasColumnName("USER_T_PARKINGPAY");

            entity.Property(e => e.UserTParkingtariffs)
                .HasColumnName("USER_T_PARKINGTARIFFS");

            entity.Property(e => e.UserTPayacc)
                .HasColumnName("USER_T_PAYACC");

            entity.Property(e => e.UserTPayaccmng)
                .HasColumnName("USER_T_PAYACCMNG");

            entity.Property(e => e.UserTPaydesk)
                .HasColumnName("USER_T_PAYDESK");

            entity.Property(e => e.UserTPaydeskAccinc)
                .HasColumnName("USER_T_PAYDESK_ACCINC");

            entity.Property(e => e.UserTPaydeskManualselect)
                .HasColumnName("USER_T_PAYDESK_MANUALSELECT");

            entity.Property(e => e.UserTPaydesklite)
                .HasColumnName("USER_T_PAYDESKLITE");

            entity.Property(e => e.UserTPaydeskliteManualsel)
                .HasColumnName("USER_T_PAYDESKLITE_MANUALSEL");

            entity.Property(e => e.UserTPayinc)
                .HasColumnName("USER_T_PAYINC");

            entity.Property(e => e.UserTPaymenu)
                .HasColumnName("USER_T_PAYMENU");

            entity.Property(e => e.UserTPersonal)
                .HasColumnName("USER_T_PERSONAL");

            entity.Property(e => e.UserTPersonalAcc)
                .HasColumnName("USER_T_PERSONAL_ACC");

            entity.Property(e => e.UserTPersonalAccess)
                .HasColumnName("USER_T_PERSONAL_ACCESS");

            entity.Property(e => e.UserTPersonalAd)
                .HasColumnName("USER_T_PERSONAL_AD");

            entity.Property(e => e.UserTPersonalAdd)
                .HasColumnName("USER_T_PERSONAL_ADD");

            entity.Property(e => e.UserTPersonalBadges)
                .HasColumnName("USER_T_PERSONAL_BADGES");

            entity.Property(e => e.UserTPersonalDel)
                .HasColumnName("USER_T_PERSONAL_DEL");

            entity.Property(e => e.UserTPersonalEdit)
                .HasColumnName("USER_T_PERSONAL_EDIT");

            entity.Property(e => e.UserTPersonalSetzone)
                .HasColumnName("USER_T_PERSONAL_SETZONE");

            entity.Property(e => e.UserTPersonalSms)
                .HasColumnName("USER_T_PERSONAL_SMS");

            entity.Property(e => e.UserTPlans)
                .HasColumnName("USER_T_PLANS");

            entity.Property(e => e.UserTReports)
                .HasColumnName("USER_T_REPORTS");

            entity.Property(e => e.UserTRules)
                .HasColumnName("USER_T_RULES");

            entity.Property(e => e.UserTSspilogin)
                .HasColumnName("USER_T_SSPILOGIN");

            entity.Property(e => e.UserTUnlockMolockers)
                .HasColumnName("USER_T_UNLOCK_MOLOCKERS");

            entity.Property(e => e.UserTUsers)
                .HasColumnName("USER_T_USERS");

            entity.Property(e => e.UserWritedb)
                .HasColumnName("USER_WRITEDB");
        });

        modelBuilder.Entity<PersonalKeys>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("personal_keys");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Codekey)
                .HasColumnName("CODEKEY");

            entity.Property(e => e.CodekeyDispFormat)
                .HasColumnName("CODEKEY_DISP_FORMAT");

            entity.Property(e => e.Codekeytime)
                .HasColumnName("CODEKEYTIME");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.Exptime)
                .HasColumnName("EXPTIME");

            entity.Property(e => e.MfUid)
                .HasColumnName("MF_UID");
        });

        modelBuilder.Entity<Personalimg>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("personalimg");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Data)
                .HasColumnName("DATA");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");

            entity.Property(e => e.GbId)
                .HasColumnName("GB_ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.OrderIdx)
                .HasColumnName("ORDER_IDX");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("photo");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Extver)
                .HasColumnName("EXTVER");

            entity.Property(e => e.HiresRaster)
                .HasColumnName("HIRES_RASTER");

            entity.Property(e => e.PreviewRaster)
                .HasColumnName("PREVIEW_RASTER");

            entity.Property(e => e.Ts)
                .HasColumnName("TS");

            entity.Property(e => e.TvaDesc)
                .HasColumnName("TVA_DESC");

            entity.Property(e => e.TvaDescts)
                .HasColumnName("TVA_DESCTS");

            entity.Property(e => e.TvaDesctype)
                .HasColumnName("TVA_DESCTYPE");
        });

        modelBuilder.Entity<Planfloors>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("planfloors");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Color)
                .HasColumnName("COLOR");

            entity.Property(e => e.Height)
                .HasColumnName("HEIGHT");

            entity.Property(e => e.Keepbgar)
                .HasColumnName("KEEPBGAR");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Orderidx)
                .HasColumnName("ORDERIDX");

            entity.Property(e => e.Scale)
                .HasColumnName("SCALE");

            entity.Property(e => e.Width)
                .HasColumnName("WIDTH");
        });

        modelBuilder.Entity<Planobjects>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("planobjects");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.AlarmColorind)
                .HasColumnName("ALARM_COLORIND");

            entity.Property(e => e.AlarmIcon)
                .HasColumnName("ALARM_ICON");

            entity.Property(e => e.Alarmid)
                .HasColumnName("ALARMID");

            entity.Property(e => e.Angle)
                .HasColumnName("ANGLE");

            entity.Property(e => e.Apid)
                .HasColumnName("APID");

            entity.Property(e => e.Autotext)
                .HasColumnName("AUTOTEXT");

            entity.Property(e => e.CamParam1)
                .HasColumnName("CAM_PARAM1");

            entity.Property(e => e.CamParam2)
                .HasColumnName("CAM_PARAM2");

            entity.Property(e => e.CamParam3)
                .HasColumnName("CAM_PARAM3");

            entity.Property(e => e.CamServerId)
                .HasColumnName("CAM_SERVER_ID");

            entity.Property(e => e.Camid)
                .HasColumnName("CAMID");

            entity.Property(e => e.FillColor)
                .HasColumnName("FILL_COLOR");

            entity.Property(e => e.Floorid)
                .HasColumnName("FLOORID");

            entity.Property(e => e.Height)
                .HasColumnName("HEIGHT");

            entity.Property(e => e.LineColor)
                .HasColumnName("LINE_COLOR");

            entity.Property(e => e.LineWidth)
                .HasColumnName("LINE_WIDTH");

            entity.Property(e => e.Param1)
                .HasColumnName("PARAM1");

            entity.Property(e => e.RenderingStyle)
                .HasColumnName("RENDERING_STYLE");

            entity.Property(e => e.TargetFloor)
                .HasColumnName("TARGET_FLOOR");

            entity.Property(e => e.Text)
                .HasColumnName("TEXT");

            entity.Property(e => e.TextFontcolor)
                .HasColumnName("TEXT_FONTCOLOR");

            entity.Property(e => e.TextFontsize)
                .HasColumnName("TEXT_FONTSIZE");

            entity.Property(e => e.TextHalign)
                .HasColumnName("TEXT_HALIGN");

            entity.Property(e => e.TextValign)
                .HasColumnName("TEXT_VALIGN");

            entity.Property(e => e.TextWordwrap)
                .HasColumnName("TEXT_WORDWRAP");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");

            entity.Property(e => e.Width)
                .HasColumnName("WIDTH");

            entity.Property(e => e.Xpos)
                .HasColumnName("XPOS");

            entity.Property(e => e.Ypos)
                .HasColumnName("YPOS");

            entity.Property(e => e.Zoneid)
                .HasColumnName("ZONEID");

            entity.Property(e => e.Zpos)
                .HasColumnName("ZPOS");
        });

        modelBuilder.Entity<Planobjectsbin>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("planobjectsbin");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Bindata)
                .HasColumnName("BINDATA");

            entity.Property(e => e.BindataTs)
                .HasColumnName("BINDATA_TS");

            entity.Property(e => e.Floorid)
                .HasColumnName("FLOORID");

            entity.Property(e => e.Objid)
                .HasColumnName("OBJID");
        });

        modelBuilder.Entity<Repbinduser>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.UserId, e.RepId });

            entity.ToTable("repbinduser");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");

            entity.Property(e => e.RepId)
                .HasColumnName("REP_ID");
        });

        modelBuilder.Entity<Reportuserdep>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.UserId, e.EmpId });

            entity.ToTable("reportuserdep");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");

            entity.Property(e => e.EmpId)
                .HasColumnName("EMP_ID");
        });

        modelBuilder.Entity<Rulebindings>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.PersonalId, e.RuleId });

            entity.ToTable("rulebindings");

            entity.Property(e => e.PersonalId)
                .HasColumnName("PERSONAL_ID");

            entity.Property(e => e.RuleId)
                .HasColumnName("RULE_ID");
        });

        modelBuilder.Entity<SaltoDoors>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.ExtDoorId);
            entity.Property(e => e.ExtDoorId).ValueGeneratedNever();

            entity.ToTable("salto_doors");

            entity.Property(e => e.ExtDoorId)
                .HasColumnName("EXT_DOOR_ID");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.IsOnline)
                .HasColumnName("IS_ONLINE");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<SaltoEncoders>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("salto_encoders");

            entity.Property(e => e.Id)
                .HasColumnName("ID");
        });

        modelBuilder.Entity<SaltoEventsCache>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("salto_events_cache");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.DoorExtId)
                .HasColumnName("DOOR_EXT_ID");

            entity.Property(e => e.EventDateTime)
                .HasColumnName("EVENT_DATE_TIME");

            entity.Property(e => e.IsExit)
                .HasColumnName("IS_EXIT");

            entity.Property(e => e.OperationId)
                .HasColumnName("OPERATION_ID");

            entity.Property(e => e.RecDateTime)
                .HasColumnName("REC_DATE_TIME");

            entity.Property(e => e.SubjType)
                .HasColumnName("SUBJ_TYPE");

            entity.Property(e => e.UserExtId)
                .HasColumnName("USER_EXT_ID");
        });

        modelBuilder.Entity<SaltoGroups>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.ExtGroupId);
            entity.Property(e => e.ExtGroupId).ValueGeneratedNever();

            entity.ToTable("salto_groups");

            entity.Property(e => e.ExtGroupId)
                .HasColumnName("EXT_GROUP_ID");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<SaltoGroupsMembership>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.ExtUserId, e.ExtGroupId });

            entity.ToTable("salto_groups_membership");

            entity.Property(e => e.ExtUserId)
                .HasColumnName("EXT_USER_ID");

            entity.Property(e => e.ExtGroupId)
                .HasColumnName("EXT_GROUP_ID");
        });

        modelBuilder.Entity<SaltoTimeZones>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.TimeZoneTableId);
            entity.Property(e => e.TimeZoneTableId).ValueGeneratedNever();

            entity.ToTable("salto_time_zones");

            entity.Property(e => e.TimeZoneTableId)
                .HasColumnName("TIME_ZONE_TABLE_ID");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<SaltoUsers>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("salto_users");

            entity.Property(e => e.ExtUserId)
                .HasColumnName("EXT_USER_ID");

            entity.Property(e => e.AuditOpenings)
                .HasColumnName("AUDIT_OPENINGS");

            entity.Property(e => e.AutoKeyEditRomcode)
                .HasColumnName("AUTO_KEY_EDIT_ROMCODE");

            entity.Property(e => e.CalendarId)
                .HasColumnName("CALENDAR_ID");

            entity.Property(e => e.CurrentKeyExists)
                .HasColumnName("CURRENT_KEY_EXISTS");

            entity.Property(e => e.CurrentKeyStatus)
                .HasColumnName("CURRENT_KEY_STATUS");

            entity.Property(e => e.CurrentRomCode)
                .HasColumnName("CURRENT_ROM_CODE");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.IsBanned)
                .HasColumnName("IS_BANNED");

            entity.Property(e => e.KeyExpirationDifferentFromUserExpiration)
                .HasColumnName("KEY_EXPIRATION_DIFFERENT_FROM_USER_EXPIRATION");

            entity.Property(e => e.KeyIsCancellableThroughBlackList)
                .HasColumnName("KEY_IS_CANCELLABLE_THROUGH_BLACK_LIST");

            entity.Property(e => e.KeyRevalidationUnitOfUpdatePeriod)
                .HasColumnName("KEY_REVALIDATION_UNIT_OF_UPDATE_PERIOD");

            entity.Property(e => e.KeyRevalidationUpdatePeriod)
                .HasColumnName("KEY_REVALIDATION_UPDATE_PERIOD");

            entity.Property(e => e.LockdownEnabled)
                .HasColumnName("LOCKDOWN_ENABLED");

            entity.Property(e => e.Office)
                .HasColumnName("OFFICE");

            entity.Property(e => e.OverrideLockdown)
                .HasColumnName("OVERRIDE_LOCKDOWN");

            entity.Property(e => e.PinCode)
                .HasColumnName("PIN_CODE");

            entity.Property(e => e.PinEnabled)
                .HasColumnName("PIN_ENABLED");

            entity.Property(e => e.UseAda)
                .HasColumnName("USE_ADA");

            entity.Property(e => e.UseAntiPassback)
                .HasColumnName("USE_ANTI_PASSBACK");

            entity.Property(e => e.UserActivation)
                .HasColumnName("USER_ACTIVATION");

            entity.Property(e => e.UserExpiration)
                .HasColumnName("USER_EXPIRATION");

            entity.Property(e => e.UserExpirationEnabled)
                .HasColumnName("USER_EXPIRATION_ENABLED");

            entity.Property(e => e.Wiegandcode)
                .HasColumnName("WIEGANDCODE");
        });

        modelBuilder.Entity<SaltoUsersAccess>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("salto_users_access");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.ExtDoorId)
                .HasColumnName("EXT_DOOR_ID");

            entity.Property(e => e.ExtUserId)
                .HasColumnName("EXT_USER_ID");

            entity.Property(e => e.ExtZoneId)
                .HasColumnName("EXT_ZONE_ID");

            entity.Property(e => e.TimeZoneTableId)
                .HasColumnName("TIME_ZONE_TABLE_ID");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<SaltoZones>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.ExtZoneId);
            entity.Property(e => e.ExtZoneId).ValueGeneratedNever();

            entity.ToTable("salto_zones");

            entity.Property(e => e.ExtZoneId)
                .HasColumnName("EXT_ZONE_ID");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Sideparamtypes>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.TableId, e.ParamIdx });

            entity.ToTable("sideparamtypes");

            entity.Property(e => e.TableId)
                .HasColumnName("TABLE_ID");

            entity.Property(e => e.ParamIdx)
                .HasColumnName("PARAM_IDX");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.OrderIdx)
                .HasColumnName("ORDER_IDX");

            entity.Property(e => e.Params)
                .HasColumnName("PARAMS");

            entity.Property(e => e.PersonalCarRelated)
                .HasColumnName("PERSONAL_CAR_RELATED");

            entity.Property(e => e.PersonalEmpRelated)
                .HasColumnName("PERSONAL_EMP_RELATED");

            entity.Property(e => e.PersonalGuestRelated)
                .HasColumnName("PERSONAL_GUEST_RELATED");

            entity.Property(e => e.Readonly)
                .HasColumnName("READONLY");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<Sideparamvalues>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.TableId, e.ObjId, e.ParamIdx });

            entity.ToTable("sideparamvalues");

            entity.Property(e => e.TableId)
                .HasColumnName("TABLE_ID");

            entity.Property(e => e.ObjId)
                .HasColumnName("OBJ_ID");

            entity.Property(e => e.ParamIdx)
                .HasColumnName("PARAM_IDX");

            entity.Property(e => e.Value)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<Sm>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("sm");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Description)
                .HasColumnName("DESCRIPTION");

            entity.Property(e => e.InitialState)
                .HasColumnName("INITIAL_STATE");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Univ)
                .HasColumnName("UNIV");
        });

        modelBuilder.Entity<Smscqueue>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("smscqueue");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Msgtext)
                .HasColumnName("MSGTEXT");

            entity.Property(e => e.Msgtype)
                .HasColumnName("MSGTYPE");

            entity.Property(e => e.Pushtime)
                .HasColumnName("PUSHTIME");

            entity.Property(e => e.Targetnumber)
                .HasColumnName("TARGETNUMBER");
        });

        modelBuilder.Entity<Smstates>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.Smid, e.Stateid });

            entity.ToTable("smstates");

            entity.Property(e => e.Smid)
                .HasColumnName("SMID");

            entity.Property(e => e.Stateid)
                .HasColumnName("STATEID");

            entity.Property(e => e.Flags)
                .HasColumnName("FLAGS");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.OutputPort)
                .HasColumnName("OUTPUT_PORT");

            entity.Property(e => e.OutputValue)
                .HasColumnName("OUTPUT_VALUE");
        });

        modelBuilder.Entity<Smtrans>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("smtrans");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");

            entity.Property(e => e.Port)
                .HasColumnName("PORT");

            entity.Property(e => e.Portvalue)
                .HasColumnName("PORTVALUE");

            entity.Property(e => e.Smid)
                .HasColumnName("SMID");

            entity.Property(e => e.SourceState)
                .HasColumnName("SOURCE_STATE");

            entity.Property(e => e.TargetState)
                .HasColumnName("TARGET_STATE");

            entity.Property(e => e.Timeparam)
                .HasColumnName("TIMEPARAM");

            entity.Property(e => e.Trtime)
                .HasColumnName("TRTIME");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<Telegramqueue>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("telegramqueue");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.ChatId)
                .HasColumnName("CHAT_ID");

            entity.Property(e => e.Msgtext)
                .HasColumnName("MSGTEXT");

            entity.Property(e => e.Pushtime)
                .HasColumnName("PUSHTIME");
        });

        modelBuilder.Entity<TrueipCards>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("trueip_cards");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.CardNo)
                .HasColumnName("CARD_NO");

            entity.Property(e => e.CardStatus)
                .HasColumnName("CARD_STATUS");

            entity.Property(e => e.CardType)
                .HasColumnName("CARD_TYPE");

            entity.Property(e => e.DevId)
                .HasColumnName("DEV_ID");

            entity.Property(e => e.RecNo)
                .HasColumnName("REC_NO");

            entity.Property(e => e.UserId)
                .HasColumnName("USER_ID");

            entity.Property(e => e.UserName)
                .HasColumnName("USER_NAME");

            entity.Property(e => e.VtoPosition)
                .HasColumnName("VTO_POSITION");
        });

        modelBuilder.Entity<Tz>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("tz");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Userlog>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("userlog");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Apid)
                .HasColumnName("APID");

            entity.Property(e => e.Clientip)
                .HasColumnName("CLIENTIP");

            entity.Property(e => e.Logtime)
                .HasColumnName("LOGTIME");

            entity.Property(e => e.Objid)
                .HasColumnName("OBJID");

            entity.Property(e => e.Text)
                .HasColumnName("TEXT");

            entity.Property(e => e.Userid)
                .HasColumnName("USERID");
        });

        modelBuilder.Entity<Waybills>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("waybills");

            entity.HasIndex(e => e.AutoId)
                .HasName("AUTO_ID");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.AutoId)
                .HasColumnName("AUTO_ID");

            entity.Property(e => e.Comment)
                .HasColumnName("COMMENT");

            entity.Property(e => e.Dateact)
                .HasColumnName("DATEACT");

            entity.Property(e => e.Datecre)
                .HasColumnName("DATECRE");

            entity.Property(e => e.Datefin)
                .HasColumnName("DATEFIN");

            entity.Property(e => e.Num)
                .HasColumnName("NUM");

            entity.Property(e => e.PersonId)
                .HasColumnName("PERSON_ID");

            entity.Property(e => e.Type)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<WorkuniversalParamb>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("workuniversal_paramb");

            entity.Property(e => e.Paramname)
                .HasColumnName("PARAMNAME");

            entity.Property(e => e.Profileid)
                .HasColumnName("PROFILEID");

            entity.Property(e => e.Value)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<WorkuniversalParami>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("workuniversal_parami");

            entity.Property(e => e.Paramname)
                .HasColumnName("PARAMNAME");

            entity.Property(e => e.Profileid)
                .HasColumnName("PROFILEID");

            entity.Property(e => e.Value)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<WorkuniversalProfiles>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("workuniversal_profiles");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Zonerulebindings>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => new { e.RuleId, e.ZoneId });

            entity.ToTable("zonerulebindings");

            entity.Property(e => e.RuleId)
                .HasColumnName("RULE_ID");

            entity.Property(e => e.ZoneId)
                .HasColumnName("ZONE_ID");
        });

        modelBuilder.Entity<Zones>(entity =>
        {
            //entity.HasNoKey();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.ToTable("zones");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.BusyoutAp)
                .HasColumnName("BUSYOUT_AP");

            entity.Property(e => e.BusyoutPort)
                .HasColumnName("BUSYOUT_PORT");

            entity.Property(e => e.Capacitylimit)
                .HasColumnName("CAPACITYLIMIT");

            entity.Property(e => e.EvacrepEvac)
                .HasColumnName("EVACREP_EVAC");

            entity.Property(e => e.EvacrepWrk)
                .HasColumnName("EVACREP_WRK");

            entity.Property(e => e.InfotabFtsn)
                .HasColumnName("INFOTAB_FTSN");

            entity.Property(e => e.Name)
                .HasColumnName("NAME");
        });
    }

    
    public virtual DbSet<Logs> Logs { get; set; }


    public virtual DbSet<Accessrules> Accessrules { get; set; }
    public virtual DbSet<Addomains> Addomains { get; set; }
    public virtual DbSet<Adobjzones> Adobjzones { get; set; }
    public virtual DbSet<Alarmlines> Alarmlines { get; set; }
    public virtual DbSet<Alarmlog> Alarmlog { get; set; }
    public virtual DbSet<Almbinduser> Almbinduser { get; set; }
    public virtual DbSet<AzDevicesdata> AzDevicesdata { get; set; }
    public virtual DbSet<Badgeitems> Badgeitems { get; set; }
    public virtual DbSet<Badges2> Badges2 { get; set; }
    public virtual DbSet<BasipDevicesdata> BasipDevicesdata { get; set; }
    public virtual DbSet<BioTemplates> BioTemplates { get; set; }
    public virtual DbSet<BleRegtokens> BleRegtokens { get; set; }
    public virtual DbSet<BsDevicesdata> BsDevicesdata { get; set; }
    public virtual DbSet<Cctvservers> Cctvservers { get; set; }
    public virtual DbSet<Devbindings> Devbindings { get; set; }
    public virtual DbSet<Devbinduser> Devbinduser { get; set; }
    public virtual DbSet<DevempidxData> DevempidxData { get; set; }
    public virtual DbSet<DevempidxMap> DevempidxMap { get; set; }
    public virtual DbSet<Deviceconfs> Deviceconfs { get; set; }
    public virtual DbSet<Devices> Devices { get; set; }
    public virtual DbSet<Devrulebindings> Devrulebindings { get; set; }
    public virtual DbSet<ElDevicesdata> ElDevicesdata { get; set; }
    public virtual DbSet<Emailqueue> Emailqueue { get; set; }
    public virtual DbSet<Evaction> Evaction { get; set; }
    public virtual DbSet<Evactionap> Evactionap { get; set; }
    public virtual DbSet<Evactionemp> Evactionemp { get; set; }
    public virtual DbSet<Evactionzone> Evactionzone { get; set; }
    public virtual DbSet<Evcond> Evcond { get; set; }
    public virtual DbSet<Evcondaction> Evcondaction { get; set; }
    public virtual DbSet<Evcondalmlines> Evcondalmlines { get; set; }
    public virtual DbSet<Evcondap> Evcondap { get; set; }
    public virtual DbSet<Evcondemp> Evcondemp { get; set; }
    public virtual DbSet<Evcondparamset> Evcondparamset { get; set; }
    public virtual DbSet<Evcondsch> Evcondsch { get; set; }
    public virtual DbSet<Execqueue> Execqueue { get; set; }
    public virtual DbSet<ExtdbsyncQueries> ExtdbsyncQueries { get; set; }
    public virtual DbSet<ExtdbsyncQueriesresults> ExtdbsyncQueriesresults { get; set; }
    public virtual DbSet<ExtdbsyncSources> ExtdbsyncSources { get; set; }
    public virtual DbSet<Floorbinduser> Floorbinduser { get; set; }
    public virtual DbSet<Frames> Frames { get; set; }
    public virtual DbSet<Gsmsmsqueue> Gsmsmsqueue { get; set; }
    public virtual DbSet<Gstappl> Gstappl { get; set; }
    public virtual DbSet<GstapplActionLog> GstapplActionLog { get; set; }
    public virtual DbSet<GstapplCar> GstapplCar { get; set; }
    public virtual DbSet<GstapplEmprolebindings> GstapplEmprolebindings { get; set; }
    public virtual DbSet<GstapplPeople> GstapplPeople { get; set; }
    public virtual DbSet<GstapplRoles> GstapplRoles { get; set; }
    public virtual DbSet<GstapplRoutes> GstapplRoutes { get; set; }
    public virtual DbSet<GstapplRoutesStages> GstapplRoutesStages { get; set; }
    public virtual DbSet<GstapplRulebindings> GstapplRulebindings { get; set; }
    public virtual DbSet<GstapplStagerolebindings> GstapplStagerolebindings { get; set; }
    public virtual DbSet<Guestbindings> Guestbindings { get; set; }
    public virtual DbSet<Guestphoto> Guestphoto { get; set; }
    public virtual DbSet<Guestrulebindings> Guestrulebindings { get; set; }
    public virtual DbSet<Ind> Ind { get; set; }
    public virtual DbSet<IndActions> IndActions { get; set; }
    public virtual DbSet<IndEvtActBinding> IndEvtActBinding { get; set; }
    public virtual DbSet<IndSounds> IndSounds { get; set; }
    public virtual DbSet<Ipcammodels> Ipcammodels { get; set; }
    public virtual DbSet<IpcammodelsChannels> IpcammodelsChannels { get; set; }
    public virtual DbSet<IsbcId> IsbcId { get; set; }
    public virtual DbSet<KrDevices> KrDevices { get; set; }
    public virtual DbSet<KrKeys> KrKeys { get; set; }
    public virtual DbSet<KrKgHolidays> KrKgHolidays { get; set; }
    public virtual DbSet<KrKgKeyAccounts> KrKgKeyAccounts { get; set; }
    public virtual DbSet<KrKgKeyAuth> KrKgKeyAuth { get; set; }
    public virtual DbSet<KrKgTexts> KrKgTexts { get; set; }
    public virtual DbSet<KrKgTz> KrKgTz { get; set; }
    public virtual DbSet<Logbuffer> Logbuffer { get; set; }
    public virtual DbSet<MifareKeyHistory> MifareKeyHistory { get; set; }
    public virtual DbSet<MiscMolockers> MiscMolockers { get; set; }
    public virtual DbSet<MiscMolockersLog> MiscMolockersLog { get; set; }
    public virtual DbSet<Monapselection> Monapselection { get; set; }
    public virtual DbSet<Monframes> Monframes { get; set; }
    public virtual DbSet<Monitemalms> Monitemalms { get; set; }
    public virtual DbSet<Monitemalmtabs> Monitemalmtabs { get; set; }
    public virtual DbSet<Monitemaps> Monitemaps { get; set; }
    public virtual DbSet<Monitememps> Monitememps { get; set; }
    public virtual DbSet<Monitems> Monitems { get; set; }
    public virtual DbSet<Monitemzones> Monitemzones { get; set; }
    public virtual DbSet<Monusers> Monusers { get; set; }
    public virtual DbSet<Monviews> Monviews { get; set; }
    public virtual DbSet<Oditems> Oditems { get; set; }
    public virtual DbSet<Odtypes> Odtypes { get; set; }
    public virtual DbSet<Onec8servers> Onec8servers { get; set; }
    public virtual DbSet<OsdpDevices> OsdpDevices { get; set; }
    public virtual DbSet<OsdpEncprofile> OsdpEncprofile { get; set; }
    public virtual DbSet<Paramb> Paramb { get; set; }
    public virtual DbSet<Parami> Parami { get; set; }
    public virtual DbSet<ParkingPassCosts> ParkingPassCosts { get; set; }
    public virtual DbSet<ParkingPasses> ParkingPasses { get; set; }
    public virtual DbSet<ParkingTariffPartDaytimes> ParkingTariffPartDaytimes { get; set; }
    public virtual DbSet<ParkingTariffParts> ParkingTariffParts { get; set; }
    public virtual DbSet<ParkingTariffs> ParkingTariffs { get; set; }
    public virtual DbSet<Patrol> Patrol { get; set; }
    public virtual DbSet<PatrolDevbindings> PatrolDevbindings { get; set; }
    public virtual DbSet<PatrolEmpbindings> PatrolEmpbindings { get; set; }
    public virtual DbSet<PatrolRt> PatrolRt { get; set; }
    public virtual DbSet<Payaccs> Payaccs { get; set; }
    public virtual DbSet<Payacctypes> Payacctypes { get; set; }
    public virtual DbSet<Paylog> Paylog { get; set; }
    public virtual DbSet<Paylogdetails> Paylogdetails { get; set; }
    public virtual DbSet<Paymenuitemacctypes> Paymenuitemacctypes { get; set; }
    public virtual DbSet<Paymenuitems> Paymenuitems { get; set; }
    public virtual DbSet<Paymenuitemsdates> Paymenuitemsdates { get; set; }
    public virtual DbSet<Paymenus> Paymenus { get; set; }
    public virtual DbSet<Paymenuusers> Paymenuusers { get; set; }
    public virtual DbSet<Personal> Personal { get; set; }
    public virtual DbSet<PersonalKeys> PersonalKeys { get; set; }
    public virtual DbSet<Personalimg> Personalimg { get; set; }
    public virtual DbSet<Photo> Photo { get; set; }
    public virtual DbSet<Planfloors> Planfloors { get; set; }
    public virtual DbSet<Planobjects> Planobjects { get; set; }
    public virtual DbSet<Planobjectsbin> Planobjectsbin { get; set; }
    public virtual DbSet<Repbinduser> Repbinduser { get; set; }
    public virtual DbSet<Reportuserdep> Reportuserdep { get; set; }
    public virtual DbSet<Rulebindings> Rulebindings { get; set; }
    public virtual DbSet<SaltoDoors> SaltoDoors { get; set; }
    public virtual DbSet<SaltoEncoders> SaltoEncoders { get; set; }
    public virtual DbSet<SaltoEventsCache> SaltoEventsCache { get; set; }
    public virtual DbSet<SaltoGroups> SaltoGroups { get; set; }
    public virtual DbSet<SaltoGroupsMembership> SaltoGroupsMembership { get; set; }
    public virtual DbSet<SaltoTimeZones> SaltoTimeZones { get; set; }
    public virtual DbSet<SaltoUsers> SaltoUsers { get; set; }
    public virtual DbSet<SaltoUsersAccess> SaltoUsersAccess { get; set; }
    public virtual DbSet<SaltoZones> SaltoZones { get; set; }
    public virtual DbSet<Sideparamtypes> Sideparamtypes { get; set; }
    public virtual DbSet<Sideparamvalues> Sideparamvalues { get; set; }
    public virtual DbSet<Sm> Sm { get; set; }
    public virtual DbSet<Smscqueue> Smscqueue { get; set; }
    public virtual DbSet<Smstates> Smstates { get; set; }
    public virtual DbSet<Smtrans> Smtrans { get; set; }
    public virtual DbSet<Telegramqueue> Telegramqueue { get; set; }
    public virtual DbSet<TrueipCards> TrueipCards { get; set; }
    public virtual DbSet<Tz> Tz { get; set; }
    public virtual DbSet<Userlog> Userlog { get; set; }
    public virtual DbSet<Waybills> Waybills { get; set; }
    public virtual DbSet<WorkuniversalParamb> WorkuniversalParamb { get; set; }
    public virtual DbSet<WorkuniversalParami> WorkuniversalParami { get; set; }
    public virtual DbSet<WorkuniversalProfiles> WorkuniversalProfiles { get; set; }
    public virtual DbSet<Zonerulebindings> Zonerulebindings { get; set; }
    public virtual DbSet<Zones> Zones { get; set; }
}

