class TriangleGrid extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [] };
        this.props.version = "draw"; //"list";
    }

    loadFromServer() {
        console.log(this.props.url);
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }

    componentDidMount() {
        this.loadFromServer();
    }

    render() {
        const triangleNodes = this.state.data.map(triangle =>
            <Triangle key={triangle.Location} data={triangle} />);

        return (
            <span>{triangleNodes}</span>
        );
    }
}

class Triangle extends React.Component {
    render() {
        var tr = this.props.data;
        var classToUse = "arrow-" + tr.CSSStyle;
        var triangleStyle = { left: tr.CSSLeft , top: tr.CSSTop };

        return (
            <div className={classToUse} style={triangleStyle} onClick={()=> alert(tr.Vertices)}>
                <p className={classToUse}>{this.props.data.Location}</p>
            </div>
        )
    }
}

class Vortex extends React.Component {

    render() {
        return <td className="point">{this.props.data}</td>;
    }
}

ReactDOM.render(<TriangleGrid url="/api/triangles" />, document.getElementById('content'));