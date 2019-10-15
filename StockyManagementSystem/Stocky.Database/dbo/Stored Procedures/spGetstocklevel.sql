
CREATE PROCEDURE [dbo].[spGetstocklevel] @sold int


AS

Select count(sID) 

 From dtStock S
 Where s.Sold = @sold

 RETURN

