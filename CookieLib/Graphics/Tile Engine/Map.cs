/// Tmx C# Loader for SFML
/// by Vinicius "Epiplon" Castanheira
/// 
/// This software is under the GPL 3.0
/// You should receive a copy of this license within the program
/// If not, check it at https://www.gnu.org/licenses/gpl-3.0.txt


using System;
using System.Xml;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System.Diagnostics;

namespace CookieLib.Graphics.TileEngine
{
		/// <summary>
		/// Represent a Tiled map. 
		/// </summary>
    public class Map {
			// XML-related Members
			public XmlDocument doc; // Should be private
			public XmlNodeList rootNodes; // Should be private
			public XmlNodeList mapNodes; // Should be private
			public XmlNodeList layerNodes; // Should be private
			public XmlNodeList objectgroupNodes; // Should be private

			// Map properties
			public float Version { get; private set; }
			public string Orientation { get; private set; }
			public int Width { get; private set; }
			public int Height { get; private set; }
			public int TileWidth { get; private set; }
			public int TileHeight { get; private set; }

			// Members that matter
			/// <summary>Each layer as a string sequence.</summary>
			private List<String> __layersstr;
			public List<String> layersStringList { 
				get {
					if (isLayersLoaded) return __layersstr;
					else throw new NotLoadedException("Layers not loaded. Call 'loadLayers()' method first");
				}
				private set { __layersstr = value; }
			}	
	 
			/// <summary> Each layer is a list of integer. Each integer number represent a tile. </summary>
			private List<Layer> __lyrs;
			public List<Layer> layers
			{
				get
				{
					if (isLayersLoaded) return __lyrs;
					else throw new NotLoadedException("Layers not loaded. Call 'loadLayers()' method first");
				}
				private set { __lyrs = value; }
			}

			/// <summary> A list of groups, each one holding a TiledObject. </summary>
			private List<ObjectGroup> __objGrp;
			public List<ObjectGroup> objectGroups { 
				get {
					if (isObjectsLoaded) return objectGroups;
					else throw new NotLoadedException("Object Groups not loaded. Call 'loadObjects()' method first");
				}
				private set { return; }
			}

			/// <summary> A list of Tiled tilesets. </summary>
			private List<Tileset> __tlsets;
			public List<Tileset> tilesets { 
				get {
					if(isTilesetsLoaded) return __tlsets;
					else throw new NotLoadedException("Tilesets not loaded. Call 'loadTilesets()' method first");
				}
				private set { __tlsets = value; } 
			}

			// Boolean values to check if stuff are loaded
			private bool isLayersLoaded;
			private bool isObjectsLoaded;
			private bool isTilesetsLoaded;

			/// <summary> Types of Tiled - CSV, XML (currently supported) or other compressed formates (unsupported). </summary>
			private documentType type;

			/// <summary>
			/// Constructs the map and loads the informations on the object.
			/// </summary>
			/// <param name="directory"> The directory of the .tmx file.</param>
			public Map(String directory) {
				loadMembers();
				loadDirectory(directory);
				loadNodes();
				loadMapProperties();
				isLayersLoaded = isObjectsLoaded = isTilesetsLoaded = false;
			}

			// Load class members
			private void loadMembers() {
				doc = new XmlDocument();							// Initialize the XmlDocument
				layersStringList = new List<string>();		// Initialize list of string layers
				layers = new List<Layer>();	// Initialize list of arrays of int
				objectGroups = new List<ObjectGroup>();
				tilesets = new List<Tileset>();
			}

			// Load document at directory
			private void loadDirectory(string directory) {
				try
				{
					doc.Load(directory);  // Load at specified directory
				}
				// Possible exceptions
				catch (System.IO.FileNotFoundException)
				{
					throw new Exception("The file " + directory + " could not been found."
					+ " Make sure the path is right or doesn't violate any acess rights.");
				}
			}

			/// <summary> Load nodes based on tag names. </summary>
			private void loadNodes() {
				rootNodes = doc.GetElementsByTagName("map");
				objectgroupNodes = doc.GetElementsByTagName("objectgroup");
				layerNodes = doc.GetElementsByTagName("layer");
				mapNodes = doc.GetElementsByTagName("tileset");
			}
			
			/// <summary> Load tilesets information </summary>
			private void loadTilesetsFromXML() {
				// Loads on mapNodes
				foreach (XmlNode node in mapNodes) {
					if (node.Name == "tileset") {
						Tileset newTileset = new Tileset();
						foreach (XmlAttribute attribute in node.Attributes) {
							switch (attribute.Name) {
								case "name": newTileset.Name = attribute.InnerText; break;
								case "firstgid": newTileset.FirstGID = int.Parse(attribute.InnerText); break;
								case "tilewidth": newTileset.TileWidth = int.Parse(attribute.InnerText); break;
								case "tileheight": newTileset.TileHeight = int.Parse(attribute.InnerText); break;
								default: break;
							}
						}
						foreach (XmlNode imageNode in node) {
								if(imageNode.Name == "image") {
								foreach (XmlAttribute imageAttribute in imageNode.Attributes) {
									switch (imageAttribute.Name) {
										case "source": newTileset.ImageSource = imageAttribute.InnerText; break;
										case "width": newTileset.ImageWidth = int.Parse(imageAttribute.InnerText); break;
										case "height": newTileset.ImageHeight = int.Parse(imageAttribute.InnerText); break;
										default: break;
									}
								}
							}
						}
						newTileset.Width = newTileset.ImageWidth / newTileset.TileWidth;
						newTileset.Height = newTileset.ImageHeight / newTileset.TileHeight;
						tilesets.Add(newTileset);
					}
				}
			}
			
			/// <summary> Load properties referring the map </summary>
			private void loadMapProperties() {
				foreach (XmlNode node in rootNodes) {
					foreach (XmlAttribute attribute in node.Attributes) {
						switch (attribute.Name)
						{
							case "version": Version = float.Parse(attribute.InnerText); break;
							case "orientation": Orientation = attribute.InnerText; break;
							case "width": Width = int.Parse(attribute.InnerText); break;
							case "height": Height = int.Parse(attribute.InnerText); break;
							case "tilewidth": TileWidth = int.Parse(attribute.InnerText); break;
							case "tileheight": TileHeight = int.Parse(attribute.InnerText); break;
							default: break;
						}
					}
				}
			}

			
			// Lots of checkings to make sure that 
			// the map is in the right format and in CSV
			private void checkFormedCSV() {
				//if (mapNodes.Count == 0) throw new MalformedMapException("Map does not contain a <map> tag.");
				//if (layerNodes.Count == 0) throw new MalformedMapException("Map does not contain a <layer> tag.");
				//if (objectgroupNodes.Count == 0) throw new MalformedMapException("Map does not contain a <objectgroup> tag.");
				// Check if it's in CVS format
				bool dataTag = false;
				bool encodingAttribute = false;
				bool csvEncoding = false;
				foreach (XmlNode layer in layerNodes) { // For each layer
					foreach (XmlNode layerChild in layer) {
						if(layerChild.Name == "data") { // If it is a data
							dataTag = true;
							if(layerChild.Attributes.Count > 0) {
								foreach (XmlAttribute attr in layerChild.Attributes) {
									if(attr.Name == "encoding" && attr.InnerText == "csv") {
										encodingAttribute = true;
										csvEncoding = true;
									}
								}
							}
						}
					}
				}
				// If all of the below conditions are true
				if (dataTag && encodingAttribute && csvEncoding) {
					type = documentType.csv;
					return; // Work's done
				}
				else { // Else, malfomed document
					string message = "Map malformed in CSV checking. Reasons: \n";
					message += "data tag is "+dataTag.ToString()+"\n";
					message += "encoding attribute is "+encodingAttribute.ToString()+"\n";
					message += "csv encoding is "+csvEncoding.ToString()+"\n";
					throw new Exception(message);
				}
			}
			
			
			/// <summary>
			/// Get a list of layers, whereas each layer is a string sequence with numbers and comma delimiters.
			/// </summary>
			/// <returns>A list of strings.</returns>
			public void loadLayersStringFromXML() {
				if(type == documentType.csv) {							// CSV only
					foreach (XmlNode layer in layerNodes) {		// In each <layer> tag
						foreach (XmlNode layerChild in layer) {	// In each <data> tag
							if(layerChild.Name == "data") {
								layersStringList.Add(layerChild.InnerText);
							}
						}
					}
				}
			}
			
			/// <summary>
			/// Get a list of Layers, whereas each layer is represented as a 
			/// </summary>
			/// <returns>A list of Layer objects.</returns>
			private void loadLayersFromXML() {
				if(type == documentType.csv) {		// CSV only
					foreach (XmlNode layer in layerNodes) {		// In each <layer> tag
						Layer newLayer = new Layer();
						// Loads the attributes
						foreach (XmlAttribute attribute in layer.Attributes) {
							switch(attribute.Name) {
								case "name": newLayer.Name = attribute.InnerText; break;
								case "width": newLayer.Width = int.Parse(attribute.InnerText); break;
								case "height": newLayer.Height = int.Parse(attribute.InnerText); break;
							}
						}
						// Now, loads the data and the properties
						foreach (XmlNode nodo in layer) {
							string [] dataResult;
							switch(nodo.Name) {
								case "data":
									dataResult = nodo.InnerText.Split(',');
									foreach (string dat in dataResult) {
										newLayer.Data.Add(int.Parse(dat));
									}
									break;
								case "properties":
									// Loading properties in tag <properties>
									foreach (XmlNode prop in nodo) {
										if(prop.Name == "property") {
											// Find the attributes for propety
											string name = "";
											string value = "";
											foreach (XmlAttribute att in prop.Attributes) {
												switch(att.Name) {
													case "name": name = att.InnerText; break;
													case "value": value = att.InnerText; break;
												}
											}
											newLayer.Properties.Add(name, value);
										}
									}
									break;
							}
						}
						layers.Add(newLayer);
					}
				}
			}

			private void loadObjectGroupsFromXML() {
				// get all object groups
				foreach (XmlNode objGroup in objectgroupNodes) {
					ObjectGroup group = new ObjectGroup();
					// Check all the attributes from the object group
					foreach (XmlAttribute groupAttribute in objGroup.Attributes) {
						if (groupAttribute.Name == "name") group.Name = groupAttribute.InnerText;
						if (groupAttribute.Name == "width") group.Width = int.Parse(groupAttribute.InnerText);
						if (groupAttribute.Name == "height") group.Height = int.Parse(groupAttribute.InnerText);
					}
					// Now checks all the objects contained
					foreach (XmlNode obj in objGroup) {
						TiledObject tiledObj = new TiledObject();
						// Check all the attributes for the object
						//Iterating throught the attributes of <object>
						foreach (XmlAttribute attribute in obj.Attributes) {
							if (attribute.Name == "name") tiledObj.Name = attribute.InnerText;
							if (attribute.Name == "type") tiledObj.Type = attribute.InnerText;
							if (attribute.Name == "x") tiledObj.X = int.Parse(attribute.InnerText);
							if (attribute.Name == "y") tiledObj.Y = int.Parse(attribute.InnerText);
							if (attribute.Name == "width") tiledObj.Width = int.Parse(attribute.InnerText);
							if (attribute.Name == "height") tiledObj.Height = int.Parse(attribute.InnerText);
						}
						// Now loads all the properties for this same <object>
						foreach (XmlNode properties in obj) {
							foreach (XmlNode property in properties) {
								string propertyName = "";
								string propertyValue = "";
								foreach (XmlAttribute propAttr in property.Attributes) {
									if (propAttr.Name == "name") propertyName = propAttr.InnerText;
									if (propAttr.Name == "value") propertyValue = propAttr.InnerText; 
								}
								tiledObj.Properties.Add(propertyName, propertyValue);
							}
						}
						group.Add(tiledObj);
					}
					objectGroups.Add(group);
				}
			}

			// An enumeration of types of .tmx document
			private enum documentType {
				csv,
				xml
			}

			/// <summary>
			/// A method called by the user to load layers
			/// </summary>
			public void loadLayers() {
				isLayersLoaded = true;
				loadLayersFromXML();
				loadLayersStringFromXML();			
			}

			/// <summary>
			/// A method called by the user to load objects
			/// </summary>
			public void loadObjects() {
				isObjectsLoaded = true;
				loadObjectGroupsFromXML();
			}

			/// <summary>
			/// A method called by the user to load tilesets
			/// </summary>
			public void loadTilesets() {
				isTilesetsLoaded = true;
				loadTilesetsFromXML();
			}	
			/// <summary>
			/// Load the images for Layers and Tilesets
			/// </summary>
			public void loadSprites() {
				tilesets.Sort(); // Sort the tileset based on his FirstGID
				// Loads the tilesets images from disk to memory
				foreach (Tileset tset in tilesets) {
					tset.ImageSFML = new Image("Content" + tset.ImageSource);
					tset.TextureSFML = new Texture(tset.ImageSFML);
				}
				// Build the image for each layer
				foreach (Layer layer in layers) {
					RenderTexture layerTexture = new RenderTexture((uint) (layer.Width*this.TileWidth), (uint) (layer.Height*this.TileHeight));
					layerTexture.SetView(new View( new Vector2f((layer.Width*this.TileWidth)/2, (layer.Height*this.TileHeight)/2), new Vector2f((layer.Width*this.TileWidth), -(layer.Height*this.TileHeight))));
					Sprite auxSprite;
					// The (x,y) coordinates IN THE LAYER, not in the spritesheet
					int xLoc = 0;
 					int yLoc = 0;
					foreach (int data in layer.Data) { // data is the number referering a tile
						if(xLoc > layer.Width - 1) {
							xLoc = 0;
							yLoc += 1;
						}
						// Select the matching tileset
						Tileset selectedTileset = null;
						foreach (Tileset tset in tilesets) {
							if (data >= tset.FirstGID) selectedTileset = tset;
							else break;
						}
						if(selectedTileset != null) {
							// coordinates on the spritesheet
							int x = 1;
							int y = 1;
							int target = 1;
							while (target < data) { // finds the coordinate
								x += 1;
								target += 1;
								if(x > selectedTileset.Width) {
									x = 1;
									y += 1;
								}
							}
							//IntRect rect = new IntRect((x*this.TileWidth)-1, (y* this.TileHeight)-1, this.TileWidth, this.TileHeight);
							IntRect rect = new IntRect((x-1)* this.TileWidth, (y-1) * this.TileHeight, selectedTileset.TileWidth, selectedTileset.TileHeight);
							auxSprite = new Sprite(selectedTileset.TextureSFML, rect);
							auxSprite.Position = new Vector2f(xLoc * this.TileWidth, yLoc * this.TileHeight);
							//auxSprite.Scale = new Vector2f(1, -1);
							layer.Sprites.Add (auxSprite);
						}
						xLoc += 1;	// Increase the location
					}
					
				}
			}			
		}
}
