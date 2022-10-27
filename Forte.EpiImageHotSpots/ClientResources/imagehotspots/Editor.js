define([
        "dojo/dom-construct",
        "dojo/text!./WidgetTemplate.html",
        "dojo/_base/declare",
        "dijit/registry",
        "dijit/_Widget",
        "dijit/_TemplatedMixin",
        "xstyle/css!./WidgetTemplate.css",
        "dojo/on",
        "dojo/dom-attr",
        "dojo/dom"],
    function (
        domConstruct,
        template,
        declare,
        registry,
        _Widget,
        _TemplatedMixin,
        resources,
        on,
        domAttr,
        dom) {
        return declare([
                _Widget,
                _TemplatedMixin],
            {
                templateString: template,
                intermediateChanges: true,
                resources: resources,
                value: null,
                imageUrl: null,
                blocks: [],
                labelIdPrefix: "label",
                labelClassName: "block-point-text",
                pointNotSetClassName: "point-not-set",
                constructor: function () {
                },
                onChange: function (value) {
                },
                postCreate: function () {
                    this.inherited(arguments);
                },
                startup: function () {
                    if (this.imageUrl == null) {
                        this.noimage.className = 'show';
                        this.image.className = 'hide';
                    } else {
                        this.noimage.className = 'hide';

                        if (this.blocks != null) {
                            this._initializeBlockPoints();
                        }
                    }
                },

                _initializeBlockPoints() {
                    for (const block of this.blocks) {
                        let container = domConstruct.create('div', {
                            "class": "block-point-container",
                            id: `${block.contentReference}`,
                            draggable: true,
                            style: {
                                top: `${block.y || 50}%`,
                                left: `${block.x || 50}%`,
                            }
                        })

                        let point = domConstruct.create('div', {
                            "class": "block-point"
                        })
                        let label = domConstruct.create('span', {
                            innerHTML: this._getBlockName(block),
                            "class": this._getLabelClassName(block),
                            id: `${this.labelIdPrefix}-${block.contentReference}`,
                            "data-name": block.name
                        });

                        domConstruct.place(label, container);
                        domConstruct.place(point, container);
                        domConstruct.place(container, this.canvas);
                        on(container, "dragend", e => this._handleDragEnd(e, container))
                    }
                },

                _handleDragEnd(e, target) {

                    const rec = this.image.getBoundingClientRect()

                    let percentLeft = (e.clientX - rec.left) / rec.width * 100;
                    let percentTop = (e.clientY - rec.top) / rec.height * 100;

                    percentLeft = percentLeft > 100 ? 100 : percentLeft < 0 ? 0 : percentLeft;
                    percentTop = percentTop > 100 ? 100 : percentTop < 0 ? 0 : percentTop;

                    target.style.top = `${percentTop}%`;
                    target.style.left = `${percentLeft}%`;

                    let array = [];
                    if (this.value != null) {
                        array = this.value.value
                            .filter(x => this.blocks.some(b => b.contentReference == x.contentReference))
                            .filter(x => x.contentReference != target.id);
                    }
                    array.push({
                            ContentReference: target.id,
                            X: percentLeft,
                            Y: percentTop
                        }
                    )
                    const value = {
                        value: array
                    }
                    this._set("Value", value);
                    this.onChange(value);
                    this._setLabel(`${this.labelIdPrefix}-${target.id}`);
                },
                
                _getBlockName(block) {
                  let name = block.name;
                  if (block.x == null || block.y == null) {
                    name += " (position not set)";
                  }
                  return name;
                },
              
                _getLabelClassName(block) {
                  if (block.x == null || block.y == null) {
                    return `${this.labelClassName} ${this.pointNotSetClassName}`
                  }
                  return this.labelClassName;
                },
              
                _setLabel(id) {
                  let element = dom.byId(id);
                  element.innerHTML = domAttr.get(element, "data-name");
                  domAttr.set(element, "class", this.labelClassName);
                }
            });
    });
