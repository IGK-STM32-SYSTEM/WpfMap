
                        {
                            //1.避开这个下右继续搜下右
                            pt = new Point(startPt.X, endPt.Y);
vs2 = ProcessString(index, pt, ProcessState.DownRight, range);
                            //2.从终点开始搜右下
                            if (vs2 == null)
                            {
                                //向右找标签
                                int id = Base.FindRFID.Right(index, endPt, new Base.Range());
                                if (id != -1)
                                {
                                    range = new Base.Range();
                                    range.MaxX = MapElement.MapObject.RFIDS[id].LeftPoint.X;
                                }
                                vs2 = ProcessString(index, endPt, ProcessState.RightUp, range);
                                //将前面的部分补齐
                                if (vs2 != null)
                                {
                                    vs2.InsertRange(0, vs1.GetRange(0, i + 1));
                                    vs3 = ProcessString(index, endPt, ProcessState.RightDown, range);
                                    if (vs3 != null)
                                    {
                                        vs3.InsertRange(0, vs1.GetRange(0, i + 1));
                                        if (vs3.Equals(vs2))
                                            vs3 = null;
                                    }
                                }
                            }
                            else
                            {
                                //将前面的部分补齐
                                vs2.InsertRange(0, vs1.GetRange(0, i + 1));
                                vs2.RemoveAt(i);
                            }
                            if (vs2 != null && vs2.Last() != vs1.Last())
                                return;
                            else
                                break;
                        }