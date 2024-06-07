export type FilterProps = {
  type: string;
  field: string;
  value: string;
};

export const types = [
  { value: "=", label: "Tìm Kiếm Đúng" },
  { value: "@", label: "Tìm Kiếm Gần Đúng" },
  { value: ">", label: "Tìm Kiếm Lớn Hơn" },
  { value: "<", label: "Tìm Kiếm Nhỏ Hơn" },
];
