using Microsoft.Data.SqlClient;



namespace lab_10
{
    public partial class MainPage : ContentPage
    {

        List<Pizza> Pizzas = new List<Pizza>();
        public MainPage()
        {
            InitializeComponent();
            Pizzas.Add(new Pizza { Title = "Pepperoni", Price = 87, ImageUrl = "p1.png", quant = 0 });
            Pizzas.Add(new Pizza { Title = "Cheese", Price = 99, ImageUrl = "p2.png", quant = 0 });
            Pizzas.Add(new Pizza { Title = "Neapolitan", Price = 44, ImageUrl = "p3.png", quant = 0 });
            Pizzas.Add(new Pizza { Title = "Soppressata", Price = 22, ImageUrl = "p4.png", quant = 0 });
            Pizzas.Add(new Pizza { Title = "Garlic", Price = 56, ImageUrl = "p5.png", quant = 0 });
            Pizzas.Add(new Pizza { Title = "Cola", Price = 77, ImageUrl = "p6.png", quant = 0 });
            collView.ItemsSource = Pizzas;


        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            int tot = 0;
            foreach (var pizza in Pizzas.ToList())
            {
                if (pizza.quant > 0) tot = tot + (pizza.quant * pizza.Price);
            }
            SqlConnection con = new SqlConnection("Data Source=SQL9001.site4now.net;User ID=db_aaf4eb_fahad12x_admin;Password=QWERTasdfg12x");
            string sql = "";
            string custname = "abdulaziz";
            sql = "insert into porder (custname,orderdate,total) values ( '" + custname + "' , '" + DateTime.Today.ToString("yyyy-MM-dd") + "', '" + tot + "'  )";
                             
            con.Open();
            
            
            SqlCommand comm = new SqlCommand(sql, con);
            
            comm.ExecuteNonQuery();
            
            con.Close();
            
            sql = "select * from porder  where custname= '" + custname + "'  order by id desc ";
            int ordid = 0;
            comm = new SqlCommand(sql, con);
           
            con.Open();
            SqlDataReader reader = comm.ExecuteReader();
           
            if (reader.Read())
            { ordid = (int)reader["id"]; }
            
              reader.Close();
            con.Close();
              foreach (var pizza in Pizzas.ToList())
            {
                if (pizza.quant > 0)
                {
                      sql = "insert into  porderPizz (orderid,itemname,itemquant,itemprice) values ( '" + ordid + "'  ,'" + pizza.Title + "' ,'" + pizza.quant + "','" + pizza.Price + "') ";
                    comm = new SqlCommand(sql, con);
                    con.Open();
                        comm.ExecuteNonQuery();
                    con.Close();
                }
            } 
              DisplayAlert("Alert", "Thank You for your purchase you need to pay" + tot, "OK");
        }




    }

}
