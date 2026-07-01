const express = require('express');
const cors = require('cors'); // مهم جداً
const bodyParser = require('body-parser');
const odbc = require('odbc');
const app = express();

app.use(cors()); // السماح للمتصفح بالاتصال
app.use(bodyParser.json());

app.post('/register', async (req, res) => {
    console.log("وصل طلب تسجيل جديد:", req.body); // سيظهر في شاشة السيرفر السوداء
    
    try {
        // تأكدي أن المسار أدناه هو المسار الحقيقي لملفك
        const dbPath = "C:\\Users\\اسم_جهازك\\Desktop\\project\\db.accdb"; 
        const connection = await odbc.connect(`Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=${dbPath};`);

        const query = 'INSERT INTO العملاء (اسم_العميل, رقم_العميل, البريد_الإلكتروني, كلمة_المرور) VALUES (?, ?, ?, ?)';
        
        await connection.query(query, [req.body.name, req.body.phone, req.body.email, req.body.password]);
        await connection.close();

        res.json({ success: true });
    } catch (error) {
        console.error("خطأ في قاعدة البيانات:", error);
        res.json({ success: false, error: error.message });
    }
});

app.listen(3000, () => console.log("السيرفر شغال على منفذ 3000"));