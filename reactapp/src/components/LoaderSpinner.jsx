import { Puff } from "react-loader-spinner";

export default function LoaderSpinner({wrapperClass = "", radius = "9", height = "80", width = "80"}) {
  return (
    <Puff
      height={height}
      width={width}
      color="#4fa94d"
      ariaLabel="puff-loading"
      radius={radius}
      wrapperStyle={{}}
      wrapperClass={wrapperClass}
      visible={true}
    />
  );
}