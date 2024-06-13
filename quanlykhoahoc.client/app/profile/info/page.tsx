"use client";

import { Button, Grid, TextInput } from "@mantine/core";
import { DateTimePicker } from "@mantine/dates";
import { useEffect, useState } from "react";
import {
  AccountClient,
  AuthClient,
  IUserMapping,
  UserInfoUpdate,
} from "../../web-api-client";
import { useSelector } from "react-redux";
import {
  DistrictSelect,
  ProvinceSelect,
  WardSelect,
} from "../../../components/Helper/AppSelect";
import ProfileLayout from "../../../components/Layout/ProfileLayout/ProfileLayout";
import { handleSubmit } from "../../../lib/helper";

export default function ProfileInfo() {
  const [user, setUser] = useState<IUserMapping>();

  const info = useSelector((state: any) => state.auth.user);

  useEffect(() => {
    if (info) setUser(info);
  }, [info]);

  return (
    <ProfileLayout>
      {user && (
        <Grid>
          <Grid.Col span={{ base: 12, lg: 6 }}>
            <TextInput
              label="Tên Đăng Nhập"
              type="text"
              disabled
              value={user.username}
              labelProps={{ style: { marginBottom: 6 } }}
            />
          </Grid.Col>
          <Grid.Col span={{ base: 12, lg: 6 }}>
            <TextInput
              label="Email"
              type="email"
              disabled
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
            <ProvinceSelect
              value={user.province}
              onChange={(e) => setUser((prev) => ({ ...prev, provinceId: e }))}
            />
          </Grid.Col>
          {user.provinceId && (
            <Grid.Col span={{ base: 12, lg: 6 }}>
              <DistrictSelect
                value={user.district}
                provinceId={user.provinceId}
                onChange={(e) =>
                  setUser((prev) => ({ ...prev, districtId: e }))
                }
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
            <Button
              size="xs"
              onClick={() =>
                handleSubmit(() => {
                  return new AccountClient().userInfoUpdate(
                    user as UserInfoUpdate
                  );
                }, "Sửa Thành Công")
              }
            >
              Xác Nhận
            </Button>
            {user.isActive === false && (
              <Button
                size="xs"
                ms="xs"
                color="grape"
                onClick={async () =>
                  handleSubmit(
                    () => new AuthClient().sendConfirmEmail(user.email),
                    "Gửi Thành Công"
                  )
                }
              >
                Gửi Mã Xác Nhận
              </Button>
            )}
          </Grid.Col>
        </Grid>
      )}
    </ProfileLayout>
  );
}
