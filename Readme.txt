Class Block  // 創建需要輸入顏色 ("yellow","red","blue","black","white","first")
	color->String : 磁磚顏色
	儲存圖像路徑
	GetImage()->BitMap : 讀取此block對應圖像
	get_color()->string : 獲取此block的顏色

Class Plate  // 創建需要輸入int第幾個盤子
	inside-> List<Block> : Plate中四個磁磚
	num -> int : 第幾個盤子(總七個)
	Plate : 創建物件時，從袋子中隨機選擇4個磁磚
	get_num()->int : Get num
	variousColor()->List<string> : 輸出盤中有幾種顏色
	show() : 顯現plate中的磁磚
	GetBlocksWithColor(string)->Tuple<List<Block>, List<Block>>: 拿取Plate中的磁磚
		//輸入要拿取的顏色 輸出<要放進PlayerPlate的花磚,掉到公共區的花磚>

Class PublicPate 
	inside-> List<Block> : PublicPlate(上限21)
	firstPlayerFlag -> bool: 起始玩家標誌還在不在
	PublicPate : 創建物件時，一開始有起始玩家標誌
	variousColor()->List<string> : 輸出盤中有幾種顏色
	show() : 顯現PublicPlate中的磁磚
	GetBlocksWithColor(string)->List<Block>: 拿取PublicPlate中的磁磚
		//輸入要拿取的顏色 輸出<要放進PlayerPlate的花磚>
	Insert_block(List<Block>) : 新增PublicPate的磁磚，將Plate掉下來的磁磚放進公共區

Class Row // 花磚排的每一列，創建需要輸入int第幾列
	inside-> List<Block> : 每列有的磁磚
	block_count-> int : 現在有的磁磚數量
	num -> int : 第幾列，也當作此列的最大容量數
	color->string : 現在的磁磚顏色
	Row(int) : 輸入時以零開始，(0:第一列)
	Insert_block(List<Block>)->Tuple<bool, List<Block>> : 新增Row的磁磚
		\\輸入(磁磚list) 輸出<是否放得下, 多餘花磚>
	show() : 顯現Row中的磁磚
	clear() : 未完善

class Floor_Row //地板排
	inside-> List<Block> : 地板排有的磁磚
	int block_count = 0;
	Insert_block(List<Block>)->List<Block> : 新增地板排的磁磚
		\\輸入(磁磚list) 輸出<多餘花磚>
	show() : 顯現地板排中的磁磚

//---------------------------公共參數-----------------------------------------------
//袋子中的Block
Bag->List<Block>
//七個圓盤+公共區+各個玩家盤面((first)+(second)+(third)+(fourth)+(fifth)+(floor))*3
pictrues->List<PictureBox>[23];
//七個圓盤中的Block
plates->List<Plate>
// 磚頭排
PulicArea->List<PublicPate>
// 磚頭排
Rows->List<Row>
// 地板排
FloorRow->List<Floor_Row>
int button_stage //紀錄選擇哪個區域
string block_stage //紀錄選擇哪個顏色的磁磚
//-----------------------------------------------------------------------------------

//------------------------------function-----------------------------------------------
GameForm_Load() : initiial
Round_start() : 回合開始，尚未完善(尚缺Reset)
addPictureBox() : 將Form中的pictureBox用List串起來，呼叫較方便
change_button_stage(int) : 顧名思義，想製作選取後圖片其他圖片變暗，下季待續
change_block_stage(string) : 顧名思義
player_plate_MouseClick(): 點集playerPlate特定位置會將方塊放入那邊，並且進行一系列的拿取放置那邊
	stage 1 : 判定button_stage & block_stage是否有值
	stage 2 : 依屬標點選位置判定是放在花磚排的第幾行
	stage 3 : 依button_stage & block_stage如何拿取方塊
	stage 4 : 從Plate拿->拿取會放在花磚排，另外顏色放在公共區，從花磚排溢出會掉入地板區，從地板溢出會放回Bag
		  從PublicPlate拿->拿取會放在花磚排，從花磚排溢出會掉入地板區，從地板溢出會放回Bag
	//尚有判斷區會被Picture擋住的問題，還在想怎麼解決


	