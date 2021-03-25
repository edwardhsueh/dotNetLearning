using System.Collections.Generic;
using System.Linq;
using System;
namespace Edward.tryLINQ
{

  class JoinDemonstration
  {
    class Product
    {
      public string Name { get; set; }
      public int CategoryID { get; set; }
      public override string ToString()
      {
        return "Name=" + Name + ",CategoryID=" + Convert.ToString(CategoryID);
      }
    }

    class Category
    {
      public string Name { get; set; }
      public int ID { get; set; }
      public override string ToString()
      {
        return "Name=" + Name + ",ID=" + Convert.ToString(ID);
      }

    }
    #region Data
    // Specify the first data source.
    /* #region  Data */
    List<Category> categories = new List<Category>()
        {
            new Category {Name="Beverages", ID=001},
            new Category {Name="Condiments", ID=002},
            new Category {Name="Vegetables", ID=003},
            new Category {Name="Grains", ID=004},
            new Category {Name="Fruit", ID=005}
        };

    // Specify the second data source.
    List<Product> products = new List<Product>()
        {
          new Product {Name="Cola",  CategoryID=001},
          new Product {Name="Mustard", CategoryID=002},
          new Product {Name="Tea",  CategoryID=001},
          new Product {Name="Pickles", CategoryID=002},
          new Product {Name="Carrots", CategoryID=003},
          new Product {Name="Peaches", CategoryID=005},
          new Product {Name="Bok Choy", CategoryID=003},
          new Product {Name="Melons", CategoryID=005},
        };
    #endregion

    /* #endregion */

    public void InnerJoin()
    {
      // Create the query that selects
      // a property from each element.
      var innerJoinQuery =
        from category in categories
        join prod in products on category.ID equals prod.CategoryID
        select new { Category = category.ID, Product = prod.Name };

      Console.WriteLine("InnerJoin:");
      // Execute the query. Access results
      // with a simple foreach statement.
      foreach (var item in innerJoinQuery)
      {
        Console.WriteLine("{0,-10}{1}", item.Product, item.Category);
      }
      Console.WriteLine("InnerJoin: {0} items in 1 group.", innerJoinQuery.Count());
      Console.WriteLine(System.Environment.NewLine);
    }

    public void InnerJoinAndUpdate()
    {
      // Create the query that selects
      // a property from each element.
      var innerJoinQuery =
        from category in categories
        join prod in products on category.ID equals prod.CategoryID
        select new { category, prod };

      Console.WriteLine("InnerJoin2:");
      // Execute the query. Access results
      // with a simple foreach statement.
      Console.WriteLine("*** iteration InnerJoin2 Result and make change");
      foreach (var item in innerJoinQuery)
      {
         Product prod = item.prod;
         Console.WriteLine("    {0,-10}:{1,50}", nameof(prod), prod.ToString());
        //  Console.WriteLine("{0,-10}:{0,50}", "prod", prod.ToString());
         prod.Name = prod.Name + "/InnerJoin2";
         Category category = item.category;
         Console.WriteLine("    {0,-10}:{1,50}", nameof(category), category.ToString());
         category.Name = category.Name + "/InnerJoin2";

      }
      Console.WriteLine("*** List categories Result after InterJoin2");
      foreach(var c in categories){
        Console.WriteLine("    "+c.ToString());
      }
      Console.WriteLine("*** List products Result after InterJoin2");
      foreach(var p in products){
        Console.WriteLine("    "+p.ToString());
      }

    }


    public void GroupJoin()
    {
      // This is a demonstration query to show the output
      // of a "raw" group join. A more typical group join
      // is shown in the GroupInnerJoin method.
      var groupJoinQuery =
        from category in categories
        join prod in products on category.ID equals prod.CategoryID into prodGroup
        select prodGroup;

      // Store the count of total items (for demonstration only).
      int totalItems = 0;

      Console.WriteLine("Simple GroupJoin:");

      // A nested foreach statement is required to access group items.
      foreach (var prodGrouping in groupJoinQuery)
      {
        Console.WriteLine("Group:");
        foreach (var item in prodGrouping)
        {
          totalItems++;
          Console.WriteLine("   {0,-10}{1}", item.Name, item.CategoryID);
        }
      }
      Console.WriteLine("Unshaped GroupJoin: {0} items in {1} unnamed groups", totalItems, groupJoinQuery.Count());
      Console.WriteLine(System.Environment.NewLine);
    }

    public void GroupInnerJoin()
    {
      var groupJoinQuery2 =
          from category in categories
          orderby category.ID
          join prod in products on category.ID equals prod.CategoryID into prodGroup
          select new
          {
            Category = category.Name,
            Products = from prod2 in prodGroup
                       orderby prod2.Name
                       select prod2
          };

      //Console.WriteLine("GroupInnerJoin:");
      int totalItems = 0;

      Console.WriteLine("GroupInnerJoin:");
      foreach (var productGroup in groupJoinQuery2)
      {
        Console.WriteLine(productGroup.Category);
        foreach (var prodItem in productGroup.Products)
        {
          totalItems++;
          Console.WriteLine("  {0,-10} {1}", prodItem.Name, prodItem.CategoryID);
        }
      }
      Console.WriteLine("GroupInnerJoin: {0} items in {1} named groups", totalItems, groupJoinQuery2.Count());
      Console.WriteLine(System.Environment.NewLine);
    }

    public void GroupJoin3()
    {

      var groupJoinQuery3 =
          from category in categories
          join product in products on category.ID equals product.CategoryID into prodGroup
          from prod in prodGroup
          orderby prod.CategoryID
          select new { Category = prod.CategoryID, ProductName = prod.Name };

      //Console.WriteLine("GroupInnerJoin:");
      int totalItems = 0;

      Console.WriteLine("GroupJoin3:");
      foreach (var item in groupJoinQuery3)
      {
        totalItems++;
        Console.WriteLine("   {0}:{1}", item.ProductName, item.Category);
      }

      Console.WriteLine("GroupJoin3: {0} items in 1 group", totalItems);
      Console.WriteLine(System.Environment.NewLine);
    }

    public void LeftOuterJoin()
    {
      // Create the query.
      var leftOuterQuery =
        from category in categories
        join prod in products on category.ID equals prod.CategoryID into prodGroup
        select prodGroup.DefaultIfEmpty(new Product() { Name = "Nothing!", CategoryID = category.ID });

      // Store the count of total items (for demonstration only).
      int totalItems = 0;

      Console.WriteLine("Left Outer Join:");

      // A nested foreach statement  is required to access group items
      foreach (var prodGrouping in leftOuterQuery)
      {
        Console.WriteLine("Group:");
        foreach (var item in prodGrouping)
        {
          totalItems++;
          Console.WriteLine("  {0,-10}{1}", item.Name, item.CategoryID);
        }
      }
      Console.WriteLine("LeftOuterJoin: {0} items in {1} groups", totalItems, leftOuterQuery.Count());
      Console.WriteLine(System.Environment.NewLine);
    }

    public void LeftOuterJoin2()
    {
      // Create the query.
      var leftOuterQuery2 =
        from category in categories
        join prod in products on category.ID equals prod.CategoryID into prodGroup
        from item in prodGroup.DefaultIfEmpty()
        select new { Name = item == null ? "Nothing!" : item.Name, CategoryID = category.ID };

      Console.WriteLine("LeftOuterJoin2: {0} items in 1 group", leftOuterQuery2.Count());
      // Store the count of total items
      int totalItems = 0;

      Console.WriteLine("Left Outer Join 2:");

      // Groups have been flattened.
      foreach (var item in leftOuterQuery2)
      {
        totalItems++;
        Console.WriteLine("{0,-10}{1}", item.Name, item.CategoryID);
      }
      Console.WriteLine("LeftOuterJoin2: {0} items in 1 group", totalItems);
    }
  }
  /*Output:

  InnerJoin:
  Cola      1
  Tea       1
  Mustard   2
  Pickles   2
  Carrots   3
  Bok Choy  3
  Peaches   5
  Melons    5
  InnerJoin: 8 items in 1 group.


  Unshaped GroupJoin:
  Group:
      Cola      1
      Tea       1
  Group:
      Mustard   2
      Pickles   2
  Group:
      Carrots   3
      Bok Choy  3
  Group:
  Group:
      Peaches   5
      Melons    5
  Unshaped GroupJoin: 8 items in 5 unnamed groups


  GroupInnerJoin:
  Beverages
      Cola       1
      Tea        1
  Condiments
      Mustard    2
      Pickles    2
  Vegetables
      Bok Choy   3
      Carrots    3
  Grains
  Fruit
      Melons     5
      Peaches    5
  GroupInnerJoin: 8 items in 5 named groups


  GroupJoin3:
      Cola:1
      Tea:1
      Mustard:2
      Pickles:2
      Carrots:3
      Bok Choy:3
      Peaches:5
      Melons:5
  GroupJoin3: 8 items in 1 group


  Left Outer Join:
  Group:
      Cola      1
      Tea       1
  Group:
      Mustard   2
      Pickles   2
  Group:
      Carrots   3
      Bok Choy  3
  Group:
      Nothing!  4
  Group:
      Peaches   5
      Melons    5
  LeftOuterJoin: 9 items in 5 groups


  LeftOuterJoin2: 9 items in 1 group
  Left Outer Join 2:
  Cola      1
  Tea       1
  Mustard   2
  Pickles   2
  Carrots   3
  Bok Choy  3
  Nothing!  4
  Peaches   5
  Melons    5
  LeftOuterJoin2: 9 items in 1 group
  Press any key to exit.
  */
}
