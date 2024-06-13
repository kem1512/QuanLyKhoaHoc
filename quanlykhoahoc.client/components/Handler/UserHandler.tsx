"use client";

import { Button, Checkbox, Grid, Select, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  IUserMapping,
  UserClient,
  UserStatus,
  UserUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import { DateTimePicker } from "@mantine/dates";
import {
  CertificateSelect,
  DistrictSelect,
  ProvinceSelect,
  RoleSelect,
  WardSelect,
} from "../Helper/AppSelect";
import ActionButton from "../Helper/ActionButton";

export default function UserHandler({ id }: { id?: number }) {
  const UserService = new UserClient();

  const { data } = useSWR(`/api/user/${id}`, () => UserService.getEntity(id));

  const [user, setUser] = useState<IUserMapping>({
    address: "",
    avatar: "",
    dateOfBirth: new Date(),
    username: "",
    email: "",
    fullName: "",
    isActive: true,
    userStatus: UserStatus.Active,
  });

  useEffect(() => {
    if (data) setUser(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Tên Đăng Nhập"
            placeholder="Nhập Tên Đăng Nhập"
            type="text"
            value={user.username}
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Email"
            placeholder="Nhập Email"
            type="email"
            value={user.email}
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Họ Và Tên"
            placeholder="Nhập Họ Và Tên"
            type="text"
            value={user.fullName}
            onChange={(e) =>
              setUser((prev) => ({
                ...prev,
                fullName: e.target.value,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <DateTimePicker
            label="Ngày Sinh"
            placeholder="Chọn Ngày Sinh"
            defaultValue={new Date()}
            onChange={(e) =>
              setUser((prev) => ({
                ...prev,
                dateOfBirth: e,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Ảnh Đại Diện"
            placeholder="Nhập Đường Dẫn Ảnh Đại Diện"
            type="text"
            value={user.avatar}
            onChange={(e) =>
              setUser((prev) => ({
                ...prev,
                avatar: e.target.value,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <ProvinceSelect
            value={user.province}
            onChange={(e) =>
              setUser((prev) => ({ ...prev, provinceId: e.id, province: e }))
            }
          />
        </Grid.Col>
        {user.provinceId && (
          <Grid.Col span={{ base: 12, lg: 6 }}>
            <DistrictSelect
              value={user.district}
              provinceId={user.provinceId}
              onChange={(e) => setUser((prev) => ({ ...prev, districtId: e }))}
            />
          </Grid.Col>
        )}
        {user.districtId && (
          <Grid.Col span={{ base: 12, lg: 6 }}>
            <WardSelect
              value={user.ward}
              districtId={user.districtId}
              onChange={(e) => setUser((prev) => ({ ...prev, wardId: e }))}
            />
          </Grid.Col>
        )}
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <CertificateSelect
            value={user.certificate}
            onChange={(e) =>
              setUser((prev) => ({
                ...prev,
                certificateId: e.id,
                certificate: e,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <RoleSelect
            value={user.permissions}
            onChange={(e) =>
              setUser((prev) => ({
                ...prev,
                permissions: e,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Địa Chỉ"
            placeholder="Chọn Địa Chỉ"
            value={user.address}
            onChange={(e) =>
              setUser((prev) => ({
                ...prev,
                address: e.target.value,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <Select
            label="Trạng Thái"
            placeholder="Chọn Trạng Thái"
            defaultValue={user.userStatus ?? "Active"}
            data={Object.keys(UserStatus)}
          />
        </Grid.Col>
        <Grid.Col style={{ display: "flex", justifyContent: "space-between" }}>
          <Checkbox
            label="Kích Hoạt"
            checked={user.isActive}
            onChange={() =>
              setUser((prev) => ({ ...prev, isActive: !prev.isActive }))
            }
          />

          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(() => {
                return UserService.updateEntity(user.id, user as UserUpdate);
              }, "Sửa Thành Công")
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
