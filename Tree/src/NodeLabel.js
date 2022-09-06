import React from "react";

import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import CardMedia from "@material-ui/core/CardMedia";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import Icon from "@material-ui/core/Icon";
import Avatar from "@material-ui/core/Avatar";
import CardHeader from "@material-ui/core/CardHeader";
import IconButton from "@material-ui/core/IconButton";
import MoreVertIcon from "@material-ui/icons/MoreVert";
import SvgIcon from "@material-ui/core/SvgIcon";
import Person from "@material-ui/icons/Person";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";

const useStyles = makeStyles({
  card: {
    maxWidth: 345
  },
  media: {
    height: 140
  }
});
function HomeIcon(props) {
  return (
    <SvgIcon {...props}>
      <path d="M10 20v-6h4v6h5v-8h3L12 3 2 12h3v8z" />
    </SvgIcon>
  );
}
export default class NodeLabel extends React.PureComponent {
  render() {
    const { className, nodeData } = this.props;
    //const classes=useStyles();
    return (
      <div className={className}>
        <Card className="cardview">
          <CardHeader
            onClick={(e) => e.stopPropagation()}
            avatar={
              <Avatar aria-label="Recipe" className="avatar">
                <ShoppingCartIcon /> 
              </Avatar>
            }
            title={nodeData.name}
          />

          <CardActionArea onClick={(e) => e.stopPropagation()}>
            <Typography variant="body2" color="textSecondary" component="p">
              {nodeData.attributes != null && nodeData.attributes.hasOwnProperty("number")
                ? nodeData.attributes.number
                : ""}
            </Typography>
          </CardActionArea>
          <CardActions>
            <Button
              size="small"
              color="primary"
              // onClick={(e) =>
              //   console.log((nodeData._collapsed = !nodeData._collapsed))
              // }
            >
              {nodeData._collapsed ? "Expand" : "Collapse"}
            </Button>
            {/* <Button className="flex" /> */}
          </CardActions>
        </Card>
      </div>
    );
  }
}
