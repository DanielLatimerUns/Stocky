
CREATE PROCEDURE [dbo].[rpEarningsBeforePayPal] @SDFromDate datetime,@SDToDate datetime,@PDFromDate datetime ,@PDToDate datetime,@StockID BigInt
AS

 SELECT S.sID StockID,ItemTitle Ttitle,TypeTitle Category,ST.SoldValue - St.PaypayFees - ST.PandP Earning
 FROM dtSaleTransaction ST

LEFT JOiN dtStock S on ST.StockID = S.sID
INNER JOiN dtUsers U ON ST.SoldBy = U.uID
Inner JOiN dtSTockType SC ON S.ItemTypeID = SC.itID

WHERE ST.SoldDate between @SDFromDate and @SDToDate 

and S.PurchesedDate between @PDFromDate and @PDToDate

and S.sID = ISNULL(@StockID,Sid)

