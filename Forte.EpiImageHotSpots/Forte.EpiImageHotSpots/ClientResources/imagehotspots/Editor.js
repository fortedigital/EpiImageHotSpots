define([
        "dojo/dom-construct",
        "dojo/text!./WidgetTemplate.html",
        "dojo/_base/declare",
        "dijit/registry",
        "dijit/_Widget",
        "dijit/_TemplatedMixin",
        "xstyle/css!./WidgetTemplate.css",
        "dojo/on"],
    function (
        domConstruct,
        template,
        declare,
        registry,
        _Widget,
        _TemplatedMixin,
        resources,
        on) {
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
                            innerHTML: block.name,
                            "class": "block-point-text"
                        });

                        domConstruct.place(point, container);
                        domConstruct.place(label, container);
                        domConstruct.place(container, this.canvas);
                        on(container, "dragend", e => this._handleDragEnd(e, container))
                    }
                },

                _handleDragEnd(e, target) {

                    const rec = this.image.getBoundingClientRect()

                    const percentLeft = (e.clientX - rec.left) / rec.width * 100;
                    const percentTop = (e.clientY - rec.top) / rec.height * 100;

                    target.style.top = `${percentTop}%`;
                    target.style.left = `${percentLeft}%`;

                    let array = [];
                    if (this.value != null) {
                        array = this.value.value
                            .filter(x => this.blocks.some(b => b.contentReference == x.contentReference))
                            .filter(x => x.contentReference != target.id);
                    }
                    array.push({
                            contentReference: target.id,
                            x: percentLeft,
                            y: percentTop
                        }
                    )
                    const value = {
                        value: array
                    }
                    this._set("value", value);
                    this.onChange(value);
                }
            });
    });
