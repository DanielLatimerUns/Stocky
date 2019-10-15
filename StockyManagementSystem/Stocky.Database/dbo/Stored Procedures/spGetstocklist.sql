CREATE PROCEDURE [dbo].[spGetstocklist] @stockid int

as

Select s.sID,s.ItemTitle,s.ItemDesc,st.TypeTitle
,s.PurchesedValue,s.PurchesedDate,
  s.Sold

   from dtStock s

    Inner join dtSTockType st on s.ItemTypeID =st.itID

	 

  Where s.sID = isnull(@stockid,sID)

  return

