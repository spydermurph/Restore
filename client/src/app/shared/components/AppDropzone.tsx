import {
  FieldValues,
  useController,
  UseControllerProps,
} from "react-hook-form";
import { useDropzone } from "react-dropzone";
import { useCallback, useEffect } from "react";
import { FormControl, FormHelperText, Typography } from "@mui/material";
import { UploadFile } from "@mui/icons-material";

type FileWithPreview = File & { preview: string };

type Props<T extends FieldValues> = {
  name: keyof T;
} & UseControllerProps<T>;

export default function AppDropzone<T extends FieldValues>(props: Props<T>) {
  const { fieldState, field } = useController({ ...props });

  const onDrop = useCallback(
    (acceptedFiles: File[]) => {
      if (acceptedFiles.length > 0) {
        const fileWithPreview: FileWithPreview = Object.assign(
          acceptedFiles[0],
          {
            preview: URL.createObjectURL(acceptedFiles[0]),
          }
        );
        field.onChange(fileWithPreview);
      }
    },
    [field]
  );

  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop,
    accept: {
      "image/*": [".jpeg", ".jpg", ".png", ".gif"],
    },
    maxSize: 1024 * 1024 * 5,
  });

  const dzStyles = {
    display: "flex",
    border: "dashed 2px #767676",
    borderColor: "#767676",
    borderRadius: "5px",
    paddingTop: "30px",
    alignItems: "center",
    height: 200,
    width: 500,
  };

  const dzActive = {
    borderColor: "green",
  };

  useEffect(() => {
    return () => {
      if (field.value?.preview) {
        URL.revokeObjectURL(field.value.preview);
      }
    };
  }, [field.value]);

  return (
    <div {...getRootProps()}>
      <FormControl
        style={isDragActive ? { ...dzStyles, ...dzActive } : dzStyles}
        error={!!fieldState.error}
      >
        <input {...getInputProps()} />
        <UploadFile sx={{ fontSize: "100px" }} />
        <Typography variant="h4">Drop image here</Typography>
        <FormHelperText>{fieldState.error?.message}</FormHelperText>
      </FormControl>
    </div>
  );
}
