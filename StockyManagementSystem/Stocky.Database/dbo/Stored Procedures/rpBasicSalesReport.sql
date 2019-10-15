
CREATE PROCEDURE rpBasicSalesReport @SDFromDate datetime = '1900-01-01',@SDToDate datetime='2900-01-01',@PDFromDate datetime ='1900-01-01',@PDToDate datetime='2900-01-01'
AS

SELECT  tID ID , S.sID StockID,ItemTitle Ttitle,ItemDesc Description,TypeTitle Category,S.PurchesedDate,SoldDate,ST.SaleMethod,ST.SoldValue SaleAMount,ST.PandP PostageFees,ST.PaypayFees 

 FROM dtSaleTransaction ST

LEFT JOiN dtStock S on ST.StockID = S.sID
INNER JOiN dtUsers U ON ST.SoldBy = U.uID
Inner JOiN dtSTockType SC ON S.ItemTypeID = SC.itID

WHERE ST.SoldDate between @SDFromDate and @SDToDate

and S.PurchesedDate between @PDFromDate and @PDToDate

